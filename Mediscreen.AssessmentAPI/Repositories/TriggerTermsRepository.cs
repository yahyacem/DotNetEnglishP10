using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mediscreen.AssessmentAPI.Repositories
{
    public class TriggerTermsRepository : ITriggerTermsRepository
    {
        private readonly IMongoCollection<TriggerTerm> _triggerTermsCollection;
        public TriggerTermsRepository(IOptions<MongoDbSettings> mongoDbConfig)
        {
            var mongoClient = new MongoClient(
            mongoDbConfig.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbConfig.Value.DatabaseName);

            _triggerTermsCollection = mongoDatabase.GetCollection<TriggerTerm>(
                mongoDbConfig.Value.TriggerTermsCollectionName);
        }

        public async Task<List<TriggerTerm>> GetAsync() =>
            await _triggerTermsCollection.Find(_ => true).ToListAsync();
        public async Task CreateAsync(TriggerTerm newTriggerTerm) =>
            await _triggerTermsCollection.InsertOneAsync(newTriggerTerm);
    }
}
