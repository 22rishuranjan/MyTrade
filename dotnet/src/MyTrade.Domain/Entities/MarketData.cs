using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;

public class MarketData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("symbol")]
    public string Symbol { get; set; }

    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }

    [BsonElement("bid")]
    public decimal Bid { get; set; }

    [BsonElement("ask")]
    public decimal Ask { get; set; }

    [BsonElement("last")]
    public decimal Last { get; set; }

    [BsonElement("volume")]
    public decimal Volume { get; set; }

    [BsonElement("high")]
    public decimal High { get; set; }

    [BsonElement("low")]
    public decimal Low { get; set; }

    [BsonElement("open")]
    public decimal Open { get; set; }

    [BsonElement("close")]
    public decimal? Close { get; set; }

    [BsonElement("source")]
    public string Source { get; set; } // Exchange name
}
