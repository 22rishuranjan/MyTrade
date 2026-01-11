using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace MyTrade.Infrastructure.Seed;

public static class DbContextSeeder
{
    /// <summary>
    /// Seeds MongoDB from JSON files under: Infrastructure/Seed/SeederJson
    /// Each file must be a JSON array. Collection name is file name without extension.
    /// Example: traders.json -> "traders" collection.
    /// </summary>
    public static async Task SeedAsync(
        IMongoDatabase database,
        ILogger logger,
        string? seedFolderAbsolutePath = null,
        bool seedOnlyIfCollectionEmpty = true,
        CancellationToken ct = default)
    {

        var folderPath = seedFolderAbsolutePath;

        if (string.IsNullOrWhiteSpace(folderPath))
        {
          
            folderPath = Path.Combine(
                AppContext.BaseDirectory, // bin/.../
                "..", "..", "..", "..", "..", // back to /src/MyTrade.API/
                "MyTrade.Infrastructure",
                "Seed",
                "SeederJson"
            );

            folderPath = Path.GetFullPath(folderPath);
        }

        if (!Directory.Exists(folderPath))
        {
            logger.LogWarning("Mongo seed folder not found: {FolderPath}. Seeding skipped.", folderPath);
            return;
        }

        var files = Directory.GetFiles(folderPath, "*.json", SearchOption.TopDirectoryOnly);

        if (files.Length == 0)
        {
            logger.LogWarning("No seed JSON files found in: {FolderPath}", folderPath);
            return;
        }

        logger.LogInformation("Starting MongoDB seeding from: {FolderPath}", folderPath);

        foreach (var file in files)
        {
            var collectionName = Path.GetFileNameWithoutExtension(file);

            try
            {
                await SeedOneCollectionAsync(
                    database,
                    file,
                    collectionName,
                    seedOnlyIfCollectionEmpty,
                    logger,
                    ct);
            }
            catch (Exception ex)
            {
                // Seeding is helpful, but shouldn't brick startup in dev.
                logger.LogError(ex, "Error seeding collection {Collection} from {File}", collectionName, file);
            }
        }

        logger.LogInformation("MongoDB seeding completed.");
    }

    private static async Task SeedOneCollectionAsync(
        IMongoDatabase database,
        string jsonFilePath,
        string collectionName,
        bool seedOnlyIfCollectionEmpty,
        ILogger logger,
        CancellationToken ct)
    {
        if (!File.Exists(jsonFilePath))
            return;

        var collection = database.GetCollection<MongoDB.Bson.BsonDocument>(collectionName);

        if (seedOnlyIfCollectionEmpty)
        {
            var existing = await collection.CountDocumentsAsync(
                FilterDefinition<MongoDB.Bson.BsonDocument>.Empty,
                cancellationToken: ct);

            if (existing > 0)
            {
                logger.LogInformation("Skipped seeding {Collection} (already has {Count} docs).", collectionName, existing);
                return;
            }
        }

        var json = await File.ReadAllTextAsync(jsonFilePath, ct);

        MongoDB.Bson.BsonArray array;
        try
        {
            // Expects JSON array: [ {..}, {..} ]
            array = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<MongoDB.Bson.BsonArray>(json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Seed file must be a JSON array: {jsonFilePath}", ex);
        }

        if (array.Count == 0)
        {
            logger.LogInformation("Skipped seeding {Collection} (file empty): {File}", collectionName, jsonFilePath);
            return;
        }

        var documents = array
            .Where(x => x.BsonType == MongoDB.Bson.BsonType.Document)
            .Select(x => x.AsBsonDocument)
            .ToList();

        if (documents.Count == 0)
        {
            logger.LogInformation("Skipped seeding {Collection} (no documents in array): {File}", collectionName, jsonFilePath);
            return;
        }

        await collection.InsertManyAsync(documents, cancellationToken: ct);

        logger.LogInformation("Seeded {Collection}: inserted {InsertedCount} docs from {File}",
            collectionName, documents.Count, Path.GetFileName(jsonFilePath));
    }
}
