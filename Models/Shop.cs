using DndBackend.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace DndBackend.Models;

public class Shop
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("items")]
    public Item[]? Items { get; set; }

}