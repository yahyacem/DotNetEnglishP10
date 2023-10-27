using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mediscreen.AssessmentAPI.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly IMongoCollection<Note> _historyCollection;
        public HistoryRepository(IOptions<MongoDbSettings> mongoDbConfig)
        {
            var mongoClient = new MongoClient(
            mongoDbConfig.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbConfig.Value.DatabaseName);

            _historyCollection = mongoDatabase.GetCollection<Note>(
                mongoDbConfig.Value.HistoryCollectionName);
        }
        public async Task<List<Note>> GetAsync(string id) =>
            await _historyCollection.Find(x => x.PatientId == id).ToListAsync();
    }
}
