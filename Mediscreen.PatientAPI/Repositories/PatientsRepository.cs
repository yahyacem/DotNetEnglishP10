using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mediscreen.PatientAPI.Repositories
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

        public async Task<List<Patient>> GetAsync() =>
            await _patientsCollection.Find(_ => true).ToListAsync();
        public async Task<Patient?> GetAsync(string id) =>
            await _patientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Patient newPatient) =>
            await _patientsCollection.InsertOneAsync(newPatient);
        public async Task UpdateAsync(string id, Patient updatedPatient) =>
            await _patientsCollection.ReplaceOneAsync(x => x.Id == id, updatedPatient);
        public async Task RemoveAsync(string id) =>
            await _patientsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
