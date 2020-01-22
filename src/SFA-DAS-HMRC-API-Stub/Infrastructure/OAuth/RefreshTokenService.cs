using IdentityServer4.Models;
using IdentityServer4.Services;
using SFA.DAS.HMRC.API.Stub.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public async Task<string> CreateRefreshTokenAsync(ClaimsPrincipal subject, Token accessToken, Client client)
        {
            return TokenUtils.GenerateToken();
        }

        public Task<string> UpdateRefreshTokenAsync(string handle, RefreshToken refreshToken, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
