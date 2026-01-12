using MyTrade.API.GraphQL;
using MyTrade.Application;
using MyTrade.Infrastructure;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Threading.RateLimiting;

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
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .ModifyRequestOptions(opt =>
            {
                opt.IncludeExceptionDetails = true;
            })
           .UsePersistedOperationPipeline()
           .AddFileSystemOperationDocumentStorage("./persisted_operations");

        return services;
    }
}
