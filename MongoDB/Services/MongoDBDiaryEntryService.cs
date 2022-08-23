using MongoDB.Bson;
using MongoDB.Driver;
using MyBlog.API.MongoDB.Models;

namespace MyBlog.API.MongoDB.Services
{
    public class MongoDBDiaryEntryService : IMongoDBService<DiaryEntry>
    {
        private const string _databaseName = "MyBlog";
        private const string _collectionName = "diaryEntries";

        private readonly IMongoCollection<DiaryEntry> _diaryEntryCollection;
        private readonly FilterDefinitionBuilder<DiaryEntry> filterBuilder = Builders<DiaryEntry>.Filter;

        public MongoDBDiaryEntryService(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
            _diaryEntryCollection = database.GetCollection<DiaryEntry>(_collectionName);
        }

        public async Task CreateItemAsync(DiaryEntry item)
        {
            await _diaryEntryCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);

            await _diaryEntryCollection.DeleteOneAsync(filter);
        }

        public async Task<DiaryEntry> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);

            return await _diaryEntryCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<DiaryEntry>> GetItemsAsync()
        {
            return await _diaryEntryCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(DiaryEntry item)
        {
            var filter = filterBuilder.Eq(diaryEntry => diaryEntry.Id, item.Id);

            await _diaryEntryCollection.ReplaceOneAsync(filter, item);
        }
    }
}
