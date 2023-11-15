using System.Collections.Generic;

namespace Mediscreen.WebApp.Services
{
    /// <summary>
    /// Class to access the constants useful in the whole app.
    /// </summary>
    public class ConstantsService
    {
        private readonly IConfiguration _configuration;
        public ConstantsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Array of scopes to authorize API calls.
        /// </summary>
        public Dictionary<string, string> Scopes
        {
            get
            {
                return _configuration.GetSection("GatewayAPI:Scopes")
                    .GetChildren()
                    .ToDictionary(x => x.Key, x => x.Value)!;
            }
        }
    }
}