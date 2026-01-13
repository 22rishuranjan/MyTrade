using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using MyTrade.API.GraphQL;
using MyTrade.Application;
using MyTrade.Infrastructure;
using System.IO.Compression;
using System.Threading.RateLimiting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyTrade.Api.Extensions;

public static class AppServiceExtension
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAppCors(configuration);

        // App layers
        services.AddInfrastructureServices(configuration);
        services.AddCoreServices();

        // Framework services
        services.AddAuthentication();
        services.AddAuthorization();
        services.AddAppResponseCompression();
        services.AddAppRateLimiting();

        services.AddGraphQL();

        return services;
    }

    public static IServiceCollection AddAppResponseCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
            {
                "application/json",
                "text/plain",
                "text/css",
                "application/javascript"
            });

            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        return services;
    }

    public static IServiceCollection AddAppCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", builder =>
            {
                builder
                    .WithOrigins(
                        "http://localhost:3002",
                        "http://localhost:3000"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    public static IServiceCollection AddAppRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter(policyName: "fixed", opt =>
            {
                opt.PermitLimit = 4;
                opt.Window = TimeSpan.FromSeconds(12);
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opt.QueueLimit = 2;
            });
        });

        return services;
    }

    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        services
        .AddGraphQLServer()
        .AddQueryType(d => d.Name(OperationTypeNames.Query))
        .AddTypeExtension<TradeQueries>()
        .UsePersistedOperationPipeline()
        .AddFileSystemOperationDocumentStorage("./persisted_operations")
        .UseExceptions()
        .UseTimeout()
        .UseDocumentCache()
        .UseRequest(next => context =>
         {
             if (IsAllowedRequest(context))
             {
                 return next(context);
             }

             var error = ErrorBuilder.New()
                 .SetMessage("only persisted operations!")
                 .Build();

             context.Result = OperationResultBuilder.CreateError(error);
             return default;
         })
        .AddHttpRequestInterceptor<CustomRequestInterceptor>();



        return services;
    }

    public class CustomRequestInterceptor : DefaultHttpRequestInterceptor
    {
        public override ValueTask OnCreateAsync(
            HttpContext context,
            IRequestExecutor requestExecutor,
            OperationRequestBuilder requestBuilder,
            CancellationToken cancellationToken)
        {
            if (context.Request.Headers.ContainsKey("admin"))
            {
                requestBuilder.SetGlobalState("admin", true);
            }

            return default;
        }
    }

    static bool IsAllowedRequest(IRequestContext context)
    {
        var isAdmin = context.ContextData.ContainsKey("admin");

        var hasInlineDocument = context.Request.Document is not null;

        var hasPersistedDocument =
            context.IsPersistedDocument ||
            context.IsCachedDocument;

        // Allow:
        // - admin requests
        // - persisted/cached operations without inline documents
        return isAdmin || (!hasInlineDocument && hasPersistedDocument);
    }
}



