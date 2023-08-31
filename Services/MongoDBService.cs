using DndBackend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DndBackend.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Item> _items;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _items = database.GetCollection<Item>(mongoDBSettings.Value.ItemsCollectionName);
    }

    public async Task<List<Item>> GetAsync()
    {
        return await _items.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Item> GetByIdAsync(string id)
    {
        FilterDefinition<Item> filter = Builders<Item>.Filter.Eq("Id", id);
        return await _items.Find(filter).FirstOrDefaultAsync();
    }
}