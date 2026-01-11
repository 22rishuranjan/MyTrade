using MyTrade.Domain.Enums;
using System.Drawing;

namespace MyTrade.API.GraphQL;

public sealed class CreateTradeInput
{
    public string TradeId { get; set; } = default!;
    public string Symbol { get; set; } = default!;

    public TradeSide Side { get; set; }

    public long Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalValue { get; set; }

    public string TraderId { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string AccountId { get; set; } = default!;

    public OrderType OrderType { get; set; }
    public TradeStatus Status { get; set; }

    public DateTime ExecutionTime { get; set; }
    public DateTime TradeDate { get; set; }
    public DateTime SettlementDate { get; set; }

    public string Currency { get; set; } = "USD";
}
