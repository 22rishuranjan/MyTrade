using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;

public class RiskLimit
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("limitId")]
    public string LimitId { get; set; }

    [BsonElement("entityType")]
    public string EntityType { get; set; } // trader, client, account

    [BsonElement("entityId")]
    public string EntityId { get; set; }

    [BsonElement("limitType")]
    [BsonRepresentation(BsonType.String)]
    public LimitType LimitType { get; set; }

    [BsonElement("threshold")]
    public decimal Threshold { get; set; }

    [BsonElement("currentValue")]
    public decimal CurrentValue { get; set; }

    [BsonElement("currency")]
    public string Currency { get; set; }

    [BsonElement("breachAlert")]
    public bool BreachAlert { get; set; }

    [BsonElement("lastChecked")]
    public DateTime LastChecked { get; set; } = DateTime.UtcNow;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
