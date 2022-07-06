using MongoDB.Driver;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Repositories;

public class ItemsRepository
{
    private const string CollectionName = "items";
    private readonly IMongoCollection<Item> _dbCollection;
    private readonly FilterDefinitionBuilder<Item> _filterDefinitionBuilder = Builders<Item>.Filter;

    public ItemsRepository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("Catalog");
        _dbCollection = database.GetCollection<Item>(CollectionName);
    }

    public async Task<IReadOnlyCollection<Item>> GetALlAsync()
    {
        return await _dbCollection.Find(_filterDefinitionBuilder.Empty).ToListAsync();
    }

    public async Task<Item> GetByIdAsync(Guid id)
    {
        var filter = _filterDefinitionBuilder.Eq(entity => entity.Id, id);
        return await _dbCollection.Find(filter).FirstOrDefaultAsync();

    }

    public async Task CreateAsync(Item entity)
    {
        if (entity==null)
        {
            throw new ArgumentException("entity can not be null");
        }

        await _dbCollection.InsertOneAsync(entity);
    }
    public async Task UpdateAsync(Item entity)
    {
        if (entity==null)
        {
            throw new ArgumentException("entity can not be null");
        }

        var filter = _filterDefinitionBuilder.Eq(x => x.Id, entity.Id);
        await _dbCollection.ReplaceOneAsync(filter,entity);
    }

    public async Task RemoveAsync(Guid guid)
    {
        var filter = _filterDefinitionBuilder.Eq(x => x.Id, guid);
        await _dbCollection.DeleteOneAsync(filter);
    }
    
    

}