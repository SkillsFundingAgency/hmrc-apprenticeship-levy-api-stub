using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using SFA.DAS.HMRC.API.Stub.Application.Commands;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth
{
    public class AuthorizationCodeStore : IAuthorizationCodeStore
    {
        private readonly IQuery<GetAuthCodeByCodeRequest, GetAuthCodeByCodeResponse> _query;
        private readonly ICommand<DeleteAuthCodeByCodeRequest, DeleteAuthCodeByCodeResponse> _delete;
        private readonly ICommand<InsertAuthCodeRequest, InsertAuthCodeResponse> _insert;

        public AuthorizationCodeStore(
            IQuery<GetAuthCodeByCodeRequest, GetAuthCodeByCodeResponse> query,
            ICommand<DeleteAuthCodeByCodeRequest, DeleteAuthCodeByCodeResponse> deleteCommand,
            ICommand<InsertAuthCodeRequest, InsertAuthCodeResponse> insertCommand)
        {
            _query = query;
            _delete = deleteCommand;
            _insert = insertCommand;
        }

        public async Task<AuthorizationCode> GetAuthorizationCodeAsync(string code)
        {
            var query = await _query.Get(new GetAuthCodeByCodeRequest
            {
                Code = code
            });

            return new AuthorizationCode
            {
                ClientId = query.AuthCode.ClientId,
                CreationTime = query.AuthCode.CreatedAt,
                RedirectUri = query.AuthCode.RedirectUri,
                RequestedScopes = new List<string> { query.AuthCode.Scope },
                Lifetime = query.AuthCode.expiresIn,
                Subject = new ClaimsPrincipal(new ClaimsIdentity(new GenericIdentity(query.AuthCode.GatewayId),
                new List<Claim>
                {
                    new Claim(JwtClaimTypes.Subject, query.AuthCode.ClientId),
                    new Claim(JwtClaimTypes.AuthenticationTime, TimeUtils.TimeSinceEpoc().ToString()),
                    new Claim(JwtClaimTypes.IdentityProvider, "hmrc-stub")
                }))
            };
        }

        public async Task RemoveAuthorizationCodeAsync(string code)
        {
            await _delete.Execute(new DeleteAuthCodeByCodeRequest
            {
                Code = code
            });
        }

        public async Task<string> StoreAuthorizationCodeAsync(AuthorizationCode code)
        {
            var token = TokenUtils.GenerateToken();
            await _insert.Execute(new InsertAuthCodeRequest
            {
                AuthCode = new Domain.AuthCode
                {
                    AuthorizationCode = token,
                    ClientId = code.ClientId,
                    CreatedAt = code.CreationTime,
                    expiresIn = code.Lifetime,
                    Scope = code.RequestedScopes.First(),
                    RedirectUri = code.RedirectUri,
                    //GatewayId = // Don't know about this
                }
            });

            return token;
        }
    }
}
