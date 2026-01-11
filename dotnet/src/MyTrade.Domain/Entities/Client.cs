using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;
public class Client
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("clientId")]
    public string ClientId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("type")]
    [BsonRepresentation(BsonType.String)]
    public ClientType Type { get; set; }

    [BsonElement("contactInfo")]
    public ContactInfo ContactInfo { get; set; }

    [BsonElement("creditRating")]
    public string CreditRating { get; set; }

    [BsonElement("riskProfile")]
    public string RiskProfile { get; set; }

    [BsonElement("accountIds")]
    public List<string> AccountIds { get; set; } = new List<string>();

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public EntityStatus Status { get; set; }

    [BsonElement("kycStatus")]
    [BsonRepresentation(BsonType.String)]
    public KycStatus KycStatus { get; set; }

    [BsonElement("onboardingDate")]
    public DateTime OnboardingDate { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
