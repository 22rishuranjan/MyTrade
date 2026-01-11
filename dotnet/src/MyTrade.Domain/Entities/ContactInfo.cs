using MyTrade.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyTrade.Domain.Entities;


public class ContactInfo
{
    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("phone")]
    public string Phone { get; set; }

    [BsonElement("address")]
    public Address Address { get; set; }
}
