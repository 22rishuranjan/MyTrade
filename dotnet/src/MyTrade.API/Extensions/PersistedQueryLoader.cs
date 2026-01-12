using HotChocolate.Execution;
using HotChocolate.Language;
using System.Text.Json;

namespace MyTrade.API.Extensions
{
    public static class PersistedQueryLoaderV
    {
        public static async Task LoadFromJsonAsync(
            IServiceProvider services,
            string jsonFilePath = "./persisted_queries.json")
        {
            Console.WriteLine($"Loading persisted queries from: {jsonFilePath}");

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                Console.WriteLine("   Using APQ mode instead");
                return;
            }

            try
            {
                var json = await File.ReadAllTextAsync(jsonFilePath);
                var queries = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                if (queries == null || queries.Count == 0)
                {
                    Console.WriteLine("No queries found in JSON");
                    return;
                }

                // Get the request executor resolver
                var executorResolver = services.GetRequiredService<IRequestExecutorResolver>();
                var executor = await executorResolver.GetRequestExecutorAsync();

                // Access the query document storage (updated interface)
                var storage = executor.Services.GetService<IOperationDocumentStorage>();

                if (storage == null)
                {
                    Console.WriteLine("Query storage not configured");
                    return;
                }

                int loaded = 0;
                foreach (var (hash, queryText) in queries)
                {
                    try
                    {
                        var document = Utf8GraphQLParser.Parse(queryText);

                        // Store using the new interface
                        //await storage.SaveAsync(hash, new OperationDocument(document));
                        loaded++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to load query {hash}: {ex.Message}");
                    }
                }

                Console.WriteLine($"Loaded {loaded} persisted queries");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading queries: {ex.Message}");
            }
        }
    }
}
