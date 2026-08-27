namespace Kayal.Api.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Company
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
   public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Domain { get; set; }
}