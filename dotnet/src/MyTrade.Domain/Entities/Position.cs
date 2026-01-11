using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;


public class Position
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("positionId")]
    public string PositionId { get; set; }

    [BsonElement("accountId")]
    public string AccountId { get; set; }

    [BsonElement("symbol")]
    public string Symbol { get; set; }

    [BsonElement("quantity")]
    public decimal Quantity { get; set; }

    [BsonElement("averagePrice")]
    public decimal AveragePrice { get; set; }

    [BsonElement("currentPrice")]
    public decimal CurrentPrice { get; set; }

    [BsonElement("marketValue")]
    public decimal MarketValue { get; set; }

    [BsonElement("unrealizedPnL")]
    public decimal UnrealizedPnL { get; set; }

    [BsonElement("realizedPnL")]
    public decimal RealizedPnL { get; set; }

    [BsonElement("currency")]
    public string Currency { get; set; }

    [BsonElement("lastUpdated")]
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
