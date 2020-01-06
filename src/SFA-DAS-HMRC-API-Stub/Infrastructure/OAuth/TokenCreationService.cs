using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using SFA.DAS.HMRC.API.Stub.Application.Commands;
using SFA.DAS.HMRC.API.Stub.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth
{
    public class TokenCreationService : ITokenCreationService
    {
        private readonly ICommand<InsertAuthRecordRequest, InsertAuthRecordResponse> _command;

        public TokenCreationService(ICommand<InsertAuthRecordRequest, InsertAuthRecordResponse> command)
        {
            _command = command;
        }

        public async Task<string> CreateTokenAsync(Token token)
        {
            var newToken = TokenUtils.GenerateToken();

            var result = await _command.Execute(new InsertAuthRecordRequest
            {
                AuthRecord = new Domain.AuthRecord
                {
                    AccessToken = newToken,
                    ClientId = token.ClientId,
                    CreatedAt = token.CreationTime,
                    ExpiresIn = token.Lifetime,
                    GatewayId = token.SubjectId,
                    IsPrivileged = false, //TODO: I don't know where this comes from
                    Scope = token.Claims.First(c => c.Type == JwtClaimTypes.Scope).Value
                }
            });

            return newToken;
        }
    }
}
