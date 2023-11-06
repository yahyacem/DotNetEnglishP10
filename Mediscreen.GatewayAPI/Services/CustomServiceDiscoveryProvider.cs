using Ocelot.ServiceDiscovery.Providers;
using Ocelot.Values;

namespace Mediscreen.GatewayAPI.Services
{
    public class CustomServiceDiscoveryProvider : IServiceDiscoveryProvider
    {
        private readonly IConfiguration _configuration;

        public CustomServiceDiscoveryProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Service> Get()
        {
            // Load service definitions from the configuration
            var services = _configuration.GetSection("ReRoutes").Get<List<Service>>()!;

            return services;
        }

        public Task<List<Service>> GetAsync()
        {
            // Load service definitions from the configuration
            var services = _configuration.GetSection("ReRoutes").Get<List<Service>>()!;

            // Simulate the completion of an asynchronous operation with a result
            return Task.FromResult(services);
        }
    }
}
