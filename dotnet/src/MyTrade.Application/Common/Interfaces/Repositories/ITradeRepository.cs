
using MyTrade.Domain.Entities;
using System.Linq.Expressions;

namespace MyTrade.Application.Common.Interfaces
{
    public interface ITradeRepository
    {
        Task<Trade> GetByIdAsync(string id);
        Task<Trade> GetByTradeIdAsync(string tradeId);
        Task<List<Trade>> GetByTraderIdAsync(string traderId, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<Trade>> GetByClientIdAsync(string clientId, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<Trade>> GetBySymbolAsync(string symbol, int limit = 100);
        Task<Trade> CreateAsync(Trade trade);
        Task<bool> UpdateAsync(string id, Trade trade);
        Task<bool> DeleteAsync(string id);
        Task<List<Trade>> FindAsync(Expression<Func<Trade, bool>> filter, int skip = 0, int limit = 100);
        Task<long> CountAsync(Expression<Func<Trade, bool>> filter);
    }
}
