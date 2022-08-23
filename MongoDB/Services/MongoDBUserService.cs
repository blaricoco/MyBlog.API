using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MyBlog.API.DTOs;
using MyBlog.API.MongoDB.Models;

namespace MyBlog.API.MongoDB.Services
{
    public class MongoDBUserService: IMongoDBService<User>
    {
        private const string _databaseName = "MyBlog";
        private const string _collectionName = "users";

        private readonly IMongoCollection<User> _userCollection;
        private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;

        public MongoDBUserService(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
            _userCollection = database.GetCollection<User>(_collectionName);
        }

        public async Task CreateItemAsync(User item)
        {
            await _userCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);

            await _userCollection.DeleteOneAsync(filter);
        }

        public async Task<User> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);

            return await _userCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetItemsAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
        }
         
        public async Task UpdateItemAsync(User item)
        {
            var filter = filterBuilder.Eq(user => user.Id, item.Id);

            await _userCollection.ReplaceOneAsync(filter, item);
        } 
    }
}
