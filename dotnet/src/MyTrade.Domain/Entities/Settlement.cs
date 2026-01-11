using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;

public class Settlement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("settlementId")]
    public string SettlementId { get; set; }

    [BsonElement("tradeId")]
    public string TradeId { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public SettlementStatus Status { get; set; }

    [BsonElement("settlementDate")]
    public DateTime SettlementDate { get; set; }

    [BsonElement("paymentAmount")]
    public decimal PaymentAmount { get; set; }

    [BsonElement("currency")]
    public string Currency { get; set; }

    [BsonElement("counterparty")]
    public string Counterparty { get; set; }

    [BsonElement("confirmationStatus")]
    public bool ConfirmationStatus { get; set; }

    [BsonElement("confirmationDate")]
    public DateTime? ConfirmationDate { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
