using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;


public class Trader
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("traderId")]
    public string TraderId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("desk")]
    public string Desk { get; set; } // equity, fx, fixed-income

    [BsonElement("permissions")]
    public List<string> Permissions { get; set; } = new List<string>();

    [BsonElement("tradingLimits")]
    public TradingLimits TradingLimits { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public EntityStatus Status { get; set; }

    [BsonElement("team")]
    public string Team { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
