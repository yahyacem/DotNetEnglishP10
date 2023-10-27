using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mediscreen.AssessmentAPI.Repositories
{
    public class PatientsRepository : IPatientsRepository
    {
        private readonly IMongoCollection<Patient> _patientsCollection;
        public PatientsRepository(IOptions<MongoDbSettings> mongoDbConfig)
        {
            var mongoClient = new MongoClient(
            mongoDbConfig.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDbConfig.Value.DatabaseName);

            _patientsCollection = mongoDatabase.GetCollection<Patient>(
                mongoDbConfig.Value.PatientsCollectionName);
        }
        public async Task<Patient?> GetAsync(string id) =>
            await _patientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task<bool> PatientExistsAsync(string id)
        {
            return (await _patientsCollection.Find(x => x.Id == id).CountDocumentsAsync()) > 0;
        }
    }
}
