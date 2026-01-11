using MyTrade.Domain.Entities;
using System.Linq.Expressions;

namespace MyTrade.Application.Services;

public interface ITradeService
{
    Task<Trade?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<Trade?> GetByTradeIdAsync(string tradeId, CancellationToken ct = default);

    Task<IReadOnlyList<Trade>> GetByTraderIdAsync(
        string traderId,
        DateTime? startDate = null,
        DateTime? endDate = null,
        CancellationToken ct = default);

    Task<IReadOnlyList<Trade>> GetByClientIdAsync(
        string clientId,
        DateTime? startDate = null,
        DateTime? endDate = null,
        CancellationToken ct = default);

    Task<IReadOnlyList<Trade>> GetBySymbolAsync(
        string symbol,
        int limit = 100,
        CancellationToken ct = default);

    Task<Trade> CreateAsync(Trade trade, CancellationToken ct = default);
    Task<bool> UpdateAsync(string id, Trade trade, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);

    Task<IReadOnlyList<Trade>> FindAsync(
        Expression<Func<Trade, bool>> filter,
        int skip = 0,
        int limit = 100,
        CancellationToken ct = default);

    Task<long> CountAsync(
        Expression<Func<Trade, bool>> filter,
        CancellationToken ct = default);
}
