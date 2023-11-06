namespace DndBackend.Models;

public class MongoDBSettings
{
    public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ItemsCollectionName { get; set; } = null!;

    public string WeaponsCollectionName { get; set; } = null!;
    public string ShopsCollectionName { get; set; } = null!;

    public string ActiveShopCollectionName { get; set; } = null!;

}