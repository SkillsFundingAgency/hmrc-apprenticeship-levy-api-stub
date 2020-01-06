using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth
{
    public class TokenCreationService : ITokenCreationService
    {
        public async Task<string> CreateTokenAsync(Token token)
        {
            return "moop[";
        }
    }
}
