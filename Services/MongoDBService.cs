using DndBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DndBackend.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Item> _items;
    private readonly IMongoCollection<Weapon> _weapons;

    private readonly IMongoCollection<Shop> _shops;
    private readonly IMongoCollection<ActiveShop> _activeShop;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _items = database.GetCollection<Item>(mongoDBSettings.Value.ItemsCollectionName);
        _weapons = database.GetCollection<Weapon>(mongoDBSettings.Value.WeaponsCollectionName);
        _shops = database.GetCollection<Shop>(mongoDBSettings.Value.ShopsCollectionName);
        _activeShop = database.GetCollection<ActiveShop>(mongoDBSettings.Value.ActiveShopCollectionName);

        try
        {
            var result = client.GetDatabase("dnd-app").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public async Task<List<Item>> GetItemsAsync()
    {
        return await _items.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Item> GetItemByIdAsync(string id)
    {
        FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", id);
        return await _items.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Weapon>> GetWeaponAsync()
    {
        return await _weapons.Find(new BsonDocument()).ToListAsync();

    }

    public async Task<Weapon> GetWeaponByIdAsync(string id)
    {
        FilterDefinition<Weapon> filter = Builders<Weapon>.Filter.Eq("Id", id);
        return await _weapons.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<Shop>> GetShopsAsync()
    {
        return await _shops.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Shop> GetShopByIdAsync(string id)
    {
        FilterDefinition<Shop> filter = Builders<Shop>.Filter.Eq("Id", id);
        return await _shops.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Shop> CreateShopAsync(Shop newShop)
    {
        await _shops.InsertOneAsync(newShop);
        return newShop;
    }

    public async Task<DeleteResult> DeleteShopAsync(string id)
    {
        var filter = Builders<Shop>.Filter.Eq(shop => shop.Id, id);

        return await _shops.DeleteOneAsync(filter);
    }


    public async Task<ActiveShop> GetActiveShopAsync()
    {
        return await _activeShop.Find(new BsonDocument()).FirstOrDefaultAsync();
    }

    public async Task SetActiveShopAsync(string shopId)

    {
        // Find the shop by ID to ensure it exists
        var shop = await GetShopByIdAsync(shopId);
        if (shop == null)
        {
            throw new KeyNotFoundException("Shop not found with the provided ID.");
        }

        // Create an ActiveShop instance from the found shop
        var newActiveShop = new ActiveShop
        {
            Id = "ACTIVE_SHOP", // Use a constant ID for the active shop
            ShopDetails = shop
        };


        // Prepare the replacement operation with upsert option to either update the existing active shop or insert if none exists
        var options = new ReplaceOptions { IsUpsert = true };
        await _activeShop.ReplaceOneAsync(
            filter: Builders<ActiveShop>.Filter.Eq(s => s.Id, newActiveShop.Id),
            replacement: newActiveShop,
            options: options);
    }


}