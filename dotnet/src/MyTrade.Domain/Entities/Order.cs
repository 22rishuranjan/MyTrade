using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("orderId")]
    public string OrderId { get; set; }

    [BsonElement("symbol")]
    public string Symbol { get; set; }

    [BsonElement("side")]
    [BsonRepresentation(BsonType.String)]
    public TradeSide Side { get; set; }

    [BsonElement("orderType")]
    [BsonRepresentation(BsonType.String)]
    public OrderType OrderType { get; set; }

    [BsonElement("quantity")]
    public decimal Quantity { get; set; }

    [BsonElement("limitPrice")]
    public decimal? LimitPrice { get; set; }

    [BsonElement("stopPrice")]
    public decimal? StopPrice { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public OrderStatus Status { get; set; }

    [BsonElement("traderId")]
    public string TraderId { get; set; }

    [BsonElement("clientId")]
    public string ClientId { get; set; }

    [BsonElement("accountId")]
    public string AccountId { get; set; }

    [BsonElement("filledQuantity")]
    public decimal FilledQuantity { get; set; }

    [BsonElement("remainingQuantity")]
    public decimal RemainingQuantity { get; set; }

    [BsonElement("averageFillPrice")]
    public decimal? AverageFillPrice { get; set; }

    [BsonElement("expiryTime")]
    public DateTime? ExpiryTime { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
