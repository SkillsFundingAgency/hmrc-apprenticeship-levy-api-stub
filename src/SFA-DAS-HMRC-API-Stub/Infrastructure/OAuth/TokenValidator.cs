using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth
{
    public class TokenValidator : ITokenValidator
    {
        protected readonly IQuery<GetAuthRecordtByRefreshTokenRequest, GetAuthRecordtByRefreshTokenResponse> AuthRecordQuery;
        protected ISystemClock Clock { get; }

        public TokenValidator(IQuery<GetAuthRecordtByRefreshTokenRequest, GetAuthRecordtByRefreshTokenResponse> authRecordQuery,
            ISystemClock clock)
        {
            AuthRecordQuery = authRecordQuery;
            Clock = clock;
        }

        public Task<TokenValidationResult> ValidateAccessTokenAsync(string token, string expectedScope = null)
        {
            throw new NotImplementedException();
        }

        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            throw new NotImplementedException();
        }

        public Task<TokenValidationResult> ValidateIdentityTokenAsync(string token, string clientId = null, bool validateLifetime = true)
        {
            throw new NotImplementedException();
        }

        public async Task<TokenValidationResult> ValidateRefreshTokenAsync(string token, Client client = null)
        {
            var record = await AuthRecordQuery.Get(new GetAuthRecordtByRefreshTokenRequest
            {
                RefreshToken = token
            });

            return new TokenValidationResult
            {
                IsError = false,
                RefreshToken = new RefreshToken
                {
                    CreationTime = Clock.UtcNow.UtcDateTime,                    
                    AccessToken = new Token
                    {                        
                        Claims = new List<Claim>
                        {
                            new Claim(JwtClaimTypes.Subject, record.AuthRecord.GatewayId),
                            new Claim(JwtClaimTypes.Scope, record.AuthRecord.Scope),
                            new Claim(JwtClaimTypes.AuthenticationTime, TimeUtils.TimeSinceEpoc().ToString()),
                        }                        
                    }                    
                }
                
            };
        }
    }
}

