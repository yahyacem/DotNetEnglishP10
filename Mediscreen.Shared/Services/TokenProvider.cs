using Microsoft.Identity.Client;

namespace Mediscreen.Shared.Services
{

    public class TokenProvider : ITokenProvider
    {
        private readonly IConfidentialClientApplication _confidentialClientApp;
        private readonly string[] _scopes;

        public TokenProvider(string clientId, string clientSecret, string authority, string[] scopes)
        {
            _confidentialClientApp = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(new Uri(authority))
                .Build();

            _scopes = scopes;
        }

        public async Task<string> GetAccessToken()
        {
            var result = await _confidentialClientApp
                .AcquireTokenForClient(_scopes)
                .ExecuteAsync();

            return result.AccessToken;
        }
    }
}
