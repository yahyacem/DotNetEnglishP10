using System;

namespace Mediscreen.Shared.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; } = null!;
        public string Port { get; set; } = null!;
        public string ConnectionString => $"mongodb://{Host}:{Port}";
        public string DatabaseName { get; set; } = null!;
        public string PatientsCollectionName { get; set; } = null!;
        public string HistoryCollectionName { get; set; } = null!;
        public string RiskLevelsCollectionName { get; set; } = null!;
        public string TriggerTermsCollectionName { get; set; } = null!;

    }
}
