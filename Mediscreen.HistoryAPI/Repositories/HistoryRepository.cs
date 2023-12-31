﻿using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mediscreen.HistoryAPI.Repositories
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
        public async Task<List<Note>> GetAsync() =>
            await _historyCollection.Find(_ => true).ToListAsync();
        public async Task<List<Note>> GetAsync(string id) =>
            await _historyCollection.Find(x => x.PatientId == id).ToListAsync();
        public async Task CreateAsync(Note newNote) =>
            await _historyCollection.InsertOneAsync(newNote);
    }
}
