using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;

public class Account
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("accountId")]
    public string AccountId { get; set; }

    [BsonElement("clientId")]
    public string ClientId { get; set; }

    [BsonElement("accountType")]
    [BsonRepresentation(BsonType.String)]
    public AccountType AccountType { get; set; }

    [BsonElement("balance")]
    public decimal Balance { get; set; }

    [BsonElement("availableBalance")]
    public decimal AvailableBalance { get; set; }

    [BsonElement("marginUsed")]
    public decimal MarginUsed { get; set; }

    [BsonElement("currency")]
    public string Currency { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public EntityStatus Status { get; set; }

    [BsonElement("positionIds")]
    public List<string> PositionIds { get; set; } = new List<string>();

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

