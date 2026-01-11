using MyTrade.Application.Common.Interfaces;
using MyTrade.Domain.Entities;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MyTrade.Infrastructure.Repositories;

public sealed class TradeRepository : ITradeRepository
{
    private const int DefaultLimit = 100;
    private const int MaxLimit = 1000;

    private readonly IMongoCollection<Trade> _trades;

    public TradeRepository(IMongoDatabase database)
    {
        _trades = database.GetCollection<Trade>("trades");
    }

    public async Task<Trade> GetByIdAsync(string id)
    {
        // If not found, returns null; interface says Trade, so we throw for correctness.
        var trade = await _trades.Find(t => t.Id == id).FirstOrDefaultAsync();
        return trade ?? throw new KeyNotFoundException($"Trade not found for Id: {id}");
    }

    public async Task<Trade> GetByTradeIdAsync(string tradeId)
    {
        var trade = await _trades.Find(t => t.TradeId == tradeId).FirstOrDefaultAsync();
        return trade ?? throw new KeyNotFoundException($"Trade not found for TradeId: {tradeId}");
    }

    public async Task<List<Trade>> GetByTraderIdAsync(
        string traderId,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var filter = Builders<Trade>.Filter.Eq(t => t.TraderId, traderId);

        if (startDate.HasValue)
            filter &= Builders<Trade>.Filter.Gte(t => t.TradeDate, startDate.Value);

        if (endDate.HasValue)
            filter &= Builders<Trade>.Filter.Lte(t => t.TradeDate, endDate.Value);

        return await _trades.Find(filter)
            .SortByDescending(t => t.ExecutionTime)
            .ToListAsync();
    }

    public async Task<List<Trade>> GetByClientIdAsync(
        string clientId,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var filter = Builders<Trade>.Filter.Eq(t => t.ClientId, clientId);

        if (startDate.HasValue)
            filter &= Builders<Trade>.Filter.Gte(t => t.TradeDate, startDate.Value);

        if (endDate.HasValue)
            filter &= Builders<Trade>.Filter.Lte(t => t.TradeDate, endDate.Value);

        return await _trades.Find(filter)
            .SortByDescending(t => t.ExecutionTime)
            .ToListAsync();
    }

    public async Task<List<Trade>> GetBySymbolAsync(string symbol, int limit = DefaultLimit)
    {
        limit = NormalizeLimit(limit);

        return await _trades.Find(t => t.Symbol == symbol)
            .SortByDescending(t => t.ExecutionTime)
            .Limit(limit)
            .ToListAsync();
    }

    public async Task<Trade> CreateAsync(Trade trade)
    {
        trade.CreatedAt = DateTime.UtcNow;
        trade.UpdatedAt = DateTime.UtcNow;

        await _trades.InsertOneAsync(trade);
        return trade;
    }

    public async Task<bool> UpdateAsync(string id, Trade trade)
    {
        trade.UpdatedAt = DateTime.UtcNow;

        // Preserve identity
        trade.Id = id;

        var result = await _trades.ReplaceOneAsync(t => t.Id == id, trade);

        // MatchedCount tells you if document existed; ModifiedCount can be 0 if identical replacement.
        return result.MatchedCount == 1;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _trades.DeleteOneAsync(t => t.Id == id);
        return result.DeletedCount == 1;
    }

    public async Task<List<Trade>> FindAsync(
        Expression<Func<Trade, bool>> filter,
        int skip = 0,
        int limit = DefaultLimit)
    {
        if (skip < 0) skip = 0;
        limit = NormalizeLimit(limit);

        return await _trades.Find(filter)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();
    }

    public async Task<long> CountAsync(Expression<Func<Trade, bool>> filter)
    {
        return await _trades.CountDocumentsAsync(filter);
    }

    private static int NormalizeLimit(int limit)
    {
        if (limit <= 0) return DefaultLimit;
        if (limit > MaxLimit) return MaxLimit;
        return limit;
    }
}
