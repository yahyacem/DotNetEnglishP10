using Mediscreen.Shared.Settings;
using AutoMapper;

namespace Mediscreen.Shared.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MediscreenDatabase"));
        }
        public static void ConfigureMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient();
        }
    }
}
