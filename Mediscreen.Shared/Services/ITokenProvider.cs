using System;
using System.Collections.Generic;
using System.Text;

namespace Mediscreen.Shared.Services
{
    public interface ITokenProvider
    {
        public Task<string> GetAccessToken();
    }
}
