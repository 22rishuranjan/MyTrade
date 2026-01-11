using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;

public class Trade
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("tradeId")]
    public string TradeId { get; set; }

    [BsonElement("symbol")]
    public string Symbol { get; set; }

    [BsonElement("side")]
    [BsonRepresentation(BsonType.String)]
    public TradeSide Side { get; set; }

    [BsonElement("quantity")]
    public decimal Quantity { get; set; }

    [BsonElement("price")]
    public decimal Price { get; set; }

    [BsonElement("totalValue")]
    public decimal TotalValue { get; set; }

    [BsonElement("traderId")]
    public string TraderId { get; set; }

    [BsonElement("clientId")]
    public string ClientId { get; set; }

    [BsonElement("accountId")]
    public string AccountId { get; set; }

    [BsonElement("orderType")]
    [BsonRepresentation(BsonType.String)]
    public OrderType OrderType { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public TradeStatus Status { get; set; }

    [BsonElement("executionTime")]
    public DateTime ExecutionTime { get; set; }

    [BsonElement("commission")]
    public decimal Commission { get; set; }

    [BsonElement("fees")]
    public decimal Fees { get; set; }

    [BsonElement("currency")]
    public string Currency { get; set; }

    [BsonElement("settlementDate")]
    public DateTime SettlementDate { get; set; }

    [BsonElement("tradeDate")]
    public DateTime TradeDate { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
