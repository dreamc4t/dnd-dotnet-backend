using DndBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DndBackend.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Item> _items;
    private readonly IMongoCollection<Weapon> _weapons;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _items = database.GetCollection<Item>(mongoDBSettings.Value.ItemsCollectionName);
        _weapons = database.GetCollection<Weapon>(mongoDBSettings.Value.WeaponsCollectionName);

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
}