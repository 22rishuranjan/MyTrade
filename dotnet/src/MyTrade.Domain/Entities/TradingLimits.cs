using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;

public class TradingLimits
{
    [BsonElement("maxOrderSize")]
    public decimal MaxOrderSize { get; set; }

    [BsonElement("maxDailyVolume")]
    public decimal MaxDailyVolume { get; set; }

    [BsonElement("maxPositionSize")]
    public decimal MaxPositionSize { get; set; }

    [BsonElement("currency")]
    public string Currency { get; set; }
}
