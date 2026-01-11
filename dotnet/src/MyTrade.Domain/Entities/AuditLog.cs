using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;

public class AuditLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("logId")]
    public string LogId { get; set; }

    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [BsonElement("userId")]
    public string UserId { get; set; }

    [BsonElement("action")]
    public string Action { get; set; }

    [BsonElement("entityType")]
    public string EntityType { get; set; }

    [BsonElement("entityId")]
    public string EntityId { get; set; }

    [BsonElement("changes")]
    public Dictionary<string, object> Changes { get; set; } = new Dictionary<string, object>();

    [BsonElement("ipAddress")]
    public string IpAddress { get; set; }

    [BsonElement("status")]
    public string Status { get; set; } // success, failed

    [BsonElement("errorMessage")]
    public string ErrorMessage { get; set; }
}
