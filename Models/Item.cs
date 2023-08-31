using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

public class Item
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("type")]
    public string Type { get; set; } = null!;

    [BsonElement("prize")]
    public string? Prize { get; set; }

    [BsonElement("description")]
    public string[] Description { get; set; } = Array.Empty<string>();

    [BsonElement("weight")]
    public string? Weight { get; set; }

    [BsonElement("link")]
    public string Link { get; set; } = null!;

    [BsonElement("tags")]
    public string[] Tags { get; set; } = Array.Empty<string>();

}