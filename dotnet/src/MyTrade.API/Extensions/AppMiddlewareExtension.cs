
using MyTrade.Infrastructure.Seed;
using MongoDB.Driver;
using MyTrade.API.Middleware;

namespace MyTrade.Api.Extensions;

public static class AppMiddlewareExtension
{
    public static void AddAppMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {

            app.UseDeveloperExceptionPage();
        }

       // app.UseCors("AllowSpecificOrigins");

        app.UseResponseCompression();
        app.UseRateLimiter();
        app.UseRouting();
        app.UseMiddleware<CustomCorsMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        // GraphQL endpoint
        app.MapGraphQL("/graphql");

    }

    public static async Task AddMigrationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        // Only seed in Development
        if (!environment.IsDevelopment())
            return;

        var seedFolderPath =
            @"C:\dev\dotnet\MyTrade\dotnet\src\MyTrade.Infrastructure\Seed\SeederJson";

        try
        {
            var db = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
            var logger = scope.ServiceProvider
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("Seeder");

            await DbContextSeeder.SeedAsync(
                database: db,
                logger: logger,
                seedFolderAbsolutePath: seedFolderPath,
                seedOnlyIfCollectionEmpty: true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Seeding failed: {ex.Message}");
            throw; // optional: rethrow in dev so you notice
        }
    }

}

