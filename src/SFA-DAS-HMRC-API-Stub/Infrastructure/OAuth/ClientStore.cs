using IdentityServer4.Models;
using IdentityServer4.Stores;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth
{
    public class ClientStore : IClientStore
    {
        private readonly string _scope = "read:apprenticeship-levy";
        private readonly string _redirectUrls = "http://localhost/auth";
        private readonly IQuery<GetApplicationByIdRequest, GetApplicationByIdResponse> _clientQuery;

        public ClientStore(IQuery<GetApplicationByIdRequest, GetApplicationByIdResponse> clientQuery)
        {
            _clientQuery = clientQuery;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var query = await _clientQuery.Get(new GetApplicationByIdRequest
            {
                ClientId = clientId
            });

            return new Client()
            {
                ClientId = query.Application.ClientID,
                ClientSecrets = { new Secret(query.Application.ClientSecret.Sha512()) },
                AllowedScopes = { _scope },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials, 
                Enabled = true,
                RedirectUris = { _redirectUrls },
                AccessTokenType = AccessTokenType.Jwt,
                AllowOfflineAccess = true
            };
        }
    }
}
