using MyTrade.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyTrade.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

     
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        builder.Logging.AddEventSourceLogger();
        builder.AddAppLogging();

        // Kestrel server configuration
        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.Limits.MaxConcurrentConnections = 15_000;
            serverOptions.Limits.MaxConcurrentUpgradedConnections = 15_000;
            serverOptions.Limits.MaxRequestBodySize = 20 * 1024 * 1024;
            serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
            serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);

            serverOptions.ListenAnyIP(5002);
            // HTTPS (uses dev cert)
            serverOptions.ListenLocalhost(7254, listenOptions =>
            {
                listenOptions.UseHttps();
            });

            // HTTPS (uses dev cert)
            serverOptions.ListenLocalhost(5111);
        });

        // App services
        builder.Services.AddAppServices(builder.Configuration);

        // Customise default API behaviour
        builder.Services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        var app = builder.Build();

        // Use app.Logger (now that the host is built)
        app.Logger.LogInformation("Application built successfully.");

        await app.AddMigrationAsync();
        app.Logger.LogInformation("DB migration applied successfully.");

        app.AddAppMiddleware();
        app.Logger.LogInformation("Middleware configured.");

        app.Logger.LogInformation("Starting the application...");
        await app.RunAsync();
    }
}
