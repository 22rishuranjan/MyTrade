using MongoDB.Driver;
using MyTrade.Application.Services;
using MyTrade.Domain.Entities;

namespace MyTrade.API.GraphQL;

[ExtendObjectType(OperationTypeNames.Query)]

public sealed class TradeQueries
{
    public Task<Trade?> TradeById(
        [Service] ITradeService tradeService,
        string id,
        CancellationToken ct) =>
        tradeService.GetByIdAsync(id, ct);

    public Task<Trade?> TradeByTradeId(
        [Service] ITradeService tradeService,
        string tradeId,
        CancellationToken ct) =>
        tradeService.GetByTradeIdAsync(tradeId, ct);

    public async Task<IReadOnlyList<Trade>> TradesByTrader(
        [Service] ITradeService tradeService,
        string traderId,
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken ct) =>
        await tradeService.GetByTraderIdAsync(traderId, startDate, endDate, ct);

    public async Task<IReadOnlyList<Trade>> TradesByClient(
        [Service] ITradeService tradeService,
        string clientId,
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken ct) =>
        await tradeService.GetByClientIdAsync(clientId, startDate, endDate, ct);

    public async Task<IReadOnlyList<Trade>> TradesBySymbol(
        [Service] ITradeService tradeService,
        string symbol,
        int limit = 100,
        CancellationToken ct = default) =>
        await tradeService.GetBySymbolAsync(symbol, limit, ct);

    [UsePaging(IncludeTotalCount = true, DefaultPageSize = 20, MaxPageSize = 200)]
    public async Task<IEnumerable<Trade>> Trades(
       [Service] IMongoDatabase db,
       int first,
       string? after,
       string? orderBy,
       CancellationToken ct)
    {
        // Use MongoDB.Driver directly for efficient paging.
        var collection = db.GetCollection<Trade>("trades");

        var sort = BuildSort(orderBy);

        // Cursor paging in Mongo: we can use executionTime+_id as a stable cursor.
        // For simplicity, we’ll implement cursor as base64 of "executionTimeTicks|id".
        // Hot Chocolate will still build edges/pageInfo; we just need to return items in order.
        var filter = FilterDefinition<Trade>.Empty;

        if (!string.IsNullOrWhiteSpace(after))
        {
            var (afterTime, afterId) = DecodeCursor(after);

            // If ordering is executionTime_DESC, fetch items strictly "before" the cursor time.
            // If ASC, fetch items "after".
            if (IsDesc(orderBy))
            {
                filter = Builders<Trade>.Filter.Or(
                    Builders<Trade>.Filter.Lt(t => t.ExecutionTime, afterTime),
                    Builders<Trade>.Filter.And(
                        Builders<Trade>.Filter.Eq(t => t.ExecutionTime, afterTime),
                        Builders<Trade>.Filter.Lt(t => t.Id, afterId)
                    )
                );
            }
            else
            {
                filter = Builders<Trade>.Filter.Or(
                    Builders<Trade>.Filter.Gt(t => t.ExecutionTime, afterTime),
                    Builders<Trade>.Filter.And(
                        Builders<Trade>.Filter.Eq(t => t.ExecutionTime, afterTime),
                        Builders<Trade>.Filter.Gt(t => t.Id, afterId)
                    )
                );
            }
        }

        // Fetch first N + 1 to let Hot Chocolate detect hasNextPage
        var pageSize = Math.Min(first, 200);

        var items = await collection
            .Find(filter)
            .Sort(sort)
            .Limit(pageSize)
            .ToListAsync(ct);

        return items;
    }

    private static SortDefinition<Trade> BuildSort(string? orderBy)
    {
        // Default: executionTime_DESC
        orderBy ??= "executionTime_DESC";

        // Support exactly what your UI sends today:
        // executionTime_DESC / executionTime_ASC
        if (orderBy.Equals("executionTime_ASC", StringComparison.OrdinalIgnoreCase))
            return Builders<Trade>.Sort.Ascending(t => t.ExecutionTime).Ascending(t => t.Id);

        // default desc
        return Builders<Trade>.Sort.Descending(t => t.ExecutionTime).Descending(t => t.Id);
    }

    private static bool IsDesc(string? orderBy)
        => string.IsNullOrWhiteSpace(orderBy) ||
           orderBy.EndsWith("_DESC", StringComparison.OrdinalIgnoreCase);

    private static (DateTime executionTime, string id) DecodeCursor(string cursor)
    {
        {
            // cursor is base64("ticks|id")
            var raw = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(cursor));
            var parts = raw.Split('|');
            if (parts.Length != 2)
                throw new GraphQLException("Invalid cursor.");

            var ticks = long.Parse(parts[0]);
            var id = parts[1];

            return (new DateTime(ticks, DateTimeKind.Utc), id);
        }
    }

}