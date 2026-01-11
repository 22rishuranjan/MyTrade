using MyTrade.Application.Services;
using MyTrade.Domain.Entities;

namespace MyTrade.API.GraphQL;

public sealed class TradeMutations
{
    public async Task<Trade> CreateTrade(
        [Service] ITradeService tradeService,
        CreateTradeInput input,
        CancellationToken ct)
    {
        var trade = new Trade
        {
            TradeId = input.TradeId,
            Symbol = input.Symbol,
            Side = input.Side,
            Quantity = input.Quantity,
            Price = input.Price,
            TotalValue = input.TotalValue,
            TraderId = input.TraderId,
            ClientId = input.ClientId,
            AccountId = input.AccountId,
            OrderType = input.OrderType,
            Status = input.Status,
            ExecutionTime = input.ExecutionTime,
            TradeDate = input.TradeDate,
            SettlementDate = input.SettlementDate,
            Currency = input.Currency
        };

        return await tradeService.CreateAsync(trade, ct);
    }

    public Task<bool> DeleteTrade(
        [Service] ITradeService tradeService,
        string id,
        CancellationToken ct) =>
        tradeService.DeleteAsync(id, ct);
}
