using DndBackend.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
namespace DndBackend.Models;



public class ActiveShop
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = "ACTIVE_SHOP";

    [BsonElement("shop")]
    public Shop? ShopDetails { get; set; }
}