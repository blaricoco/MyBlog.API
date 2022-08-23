using MongoDB.Bson;
using MongoDB.Driver;
using MyBlog.API.MongoDB.Models;

namespace MyBlog.API.MongoDB.Services
{
    public class MongoDBDiaryService: IMongoDBService<Diary>
    {
        private const string _databaseName = "MyBlog";
        private const string _collectionName = "diaries";

        private readonly IMongoCollection<Diary> _diaryCollection;
        private readonly FilterDefinitionBuilder<Diary> filterBuilder = Builders<Diary>.Filter;

        public MongoDBDiaryService(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
            _diaryCollection = database.GetCollection<Diary>(_collectionName);
        }

        public async Task CreateItemAsync(Diary item)
        {
            await _diaryCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);

            await _diaryCollection.DeleteOneAsync(filter);
        }

        public async Task<Diary> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);

            return await _diaryCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Diary>> GetItemsAsync()
        {
            return await _diaryCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Diary item)
        {
            var filter = filterBuilder.Eq(diary => diary.Id, item.Id);

            await _diaryCollection.ReplaceOneAsync(filter, item);
        }
    }
}
