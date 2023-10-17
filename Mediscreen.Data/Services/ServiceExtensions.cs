using Mediscreen.Data.Settings;
using Mediscreen.Shared.Entities;
using Mediscreen.Shared.Services;
using Microsoft.Extensions.Configuration;

namespace Mediscreen.Data.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDbSettings = configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
            services.AddIdentity<Patient, Role>()
            .AddMongoDbStores<Patient, Role, int>
            (
                mongoDbSettings.ConnectionString, mongoDbSettings.Name
            );
            services.AddControllersWithViews();
        }
    }
}
