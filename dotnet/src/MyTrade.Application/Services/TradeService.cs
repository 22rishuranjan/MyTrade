using MyTrade.Application.Common.Interfaces;
using MyTrade.Domain.Entities;
using System.Linq.Expressions;

namespace MyTrade.Application.Services;

public sealed class TradeService : ITradeService
{
    private const int DefaultLimit = 100;
    private const int MaxLimit = 1000;

    private readonly ITradeRepository _tradeRepository;

    public TradeService(ITradeRepository tradeRepository)
    {
        _tradeRepository = tradeRepository;
    }

    public async Task<Trade?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Trade id is required.", nameof(id));

        // Repository returns Trade; service returns nullable to allow "not found" patterns.
        // If your repo throws for not found, adjust accordingly.
        return await _tradeRepository.GetByIdAsync(id);
    }

    public async Task<Trade?> GetByTradeIdAsync(string tradeId, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(tradeId))
            throw new ArgumentException("TradeId is required.", nameof(tradeId));

        return await _tradeRepository.GetByTradeIdAsync(tradeId);
    }

    public async Task<IReadOnlyList<Trade>> GetByTraderIdAsync(
        string traderId,
        DateTime? startDate = null,
        DateTime? endDate = null,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(traderId))
            throw new ArgumentException("TraderId is required.", nameof(traderId));

        (startDate, endDate) = NormalizeDateRange(startDate, endDate);

        var trades = await _tradeRepository.GetByTraderIdAsync(traderId, startDate, endDate);
        return trades;
    }

    public async Task<IReadOnlyList<Trade>> GetByClientIdAsync(
        string clientId,
        DateTime? startDate = null,
        DateTime? endDate = null,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(clientId))
            throw new ArgumentException("ClientId is required.", nameof(clientId));

        (startDate, endDate) = NormalizeDateRange(startDate, endDate);

        var trades = await _tradeRepository.GetByClientIdAsync(clientId, startDate, endDate);
        return trades;
    }

    public async Task<IReadOnlyList<Trade>> GetBySymbolAsync(
        string symbol,
        int limit = DefaultLimit,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol is required.", nameof(symbol));

        limit = NormalizeLimit(limit);

        var trades = await _tradeRepository.GetBySymbolAsync(symbol.Trim().ToUpperInvariant(), limit);
        return trades;
    }

    public async Task<Trade> CreateAsync(Trade trade, CancellationToken ct = default)
    {
        if (trade is null) throw new ArgumentNullException(nameof(trade));

        ValidateTrade(trade);

        // If you generate TradeId here, do it consistently (optional).
        // trade.TradeId ??= $"TRD-{Guid.NewGuid():N}".ToUpperInvariant();

        return await _tradeRepository.CreateAsync(trade);
    }

    public async Task<bool> UpdateAsync(string id, Trade trade, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Trade id is required.", nameof(id));

        if (trade is null) throw new ArgumentNullException(nameof(trade));

        ValidateTrade(trade);

        return await _tradeRepository.UpdateAsync(id, trade);
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Trade id is required.", nameof(id));

        return await _tradeRepository.DeleteAsync(id);
    }

    public async Task<IReadOnlyList<Trade>> FindAsync(
        Expression<Func<Trade, bool>> filter,
        int skip = 0,
        int limit = DefaultLimit,
        CancellationToken ct = default)
    {
        if (filter is null) throw new ArgumentNullException(nameof(filter));

        if (skip < 0) skip = 0;
        limit = NormalizeLimit(limit);

        var trades = await _tradeRepository.FindAsync(filter, skip, limit);
        return trades;
    }

    public Task<long> CountAsync(Expression<Func<Trade, bool>> filter, CancellationToken ct = default)
    {
        if (filter is null) throw new ArgumentNullException(nameof(filter));

        return _tradeRepository.CountAsync(filter);
    }

    // -------- helpers --------

    private static void ValidateTrade(Trade trade)
    {
        // Keep this minimal; domain should enforce the heavy rules.
        if (string.IsNullOrWhiteSpace(trade.TradeId))
            throw new ArgumentException("TradeId is required.", nameof(trade));

        if (string.IsNullOrWhiteSpace(trade.Symbol))
            throw new ArgumentException("Symbol is required.", nameof(trade));

        if (trade.Quantity <= 0)
            throw new ArgumentException("Quantity must be > 0.", nameof(trade));

        if (trade.Price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(trade));

        if (string.IsNullOrWhiteSpace(trade.TraderId))
            throw new ArgumentException("TraderId is required.", nameof(trade));

        if (string.IsNullOrWhiteSpace(trade.ClientId))
            throw new ArgumentException("ClientId is required.", nameof(trade));

        if (string.IsNullOrWhiteSpace(trade.AccountId))
            throw new ArgumentException("AccountId is required.", nameof(trade));
    }

    private static (DateTime? start, DateTime? end) NormalizeDateRange(DateTime? start, DateTime? end)
    {
        if (start is null && end is null) return (null, null);

        // If only one is provided, interpret range as "that day to now/end of day"
        if (start is not null && end is null)
            end = DateTime.UtcNow;

        if (start is null && end is not null)
            start = end.Value.Date;

        // Ensure start <= end
        if (start is not null && end is not null && start > end)
            (start, end) = (end, start);

        return (start, end);
    }

    private static int NormalizeLimit(int limit)
    {
        if (limit <= 0) return DefaultLimit;
        if (limit > MaxLimit) return MaxLimit;
        return limit;
    }
}
