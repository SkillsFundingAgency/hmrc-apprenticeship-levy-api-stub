using IdentityServer4.Models;
using IdentityServer4.Stores;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth
{
    public class ResourceStore : IResourceStore
    {
        private List<IdentityResource> _identityResources;
        private readonly IQuery<GetScopeByNameRequest, GetScopeByNameResponse> _scopeQuery;
        private readonly IQuery<GetAllScopesRequest, GetAllScopesResponse> _allScopeQuery;

        public ResourceStore(
            IQuery<GetScopeByNameRequest, GetScopeByNameResponse> scopeQuery,
            IQuery<GetAllScopesRequest, GetAllScopesResponse> allScopeQuery)
        {
            _scopeQuery = scopeQuery;
            _allScopeQuery = allScopeQuery;
            _identityResources = new List<IdentityResource>();
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            return await GetApiResource(name);
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (scopeNames.Count() == 1 && scopeNames.Any(s => string.IsNullOrEmpty(s)))
            {
                var scopesResult = await _allScopeQuery.Get(new GetAllScopesRequest());

                return scopesResult.Scopes.Select(s => new ApiResource
                {
                    Name = s.Name
                });
            }

            var apiResource = await GetApiResource(scopeNames.First());

            return new List<ApiResource> { apiResource };
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return _identityResources;
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            return new Resources();
        }

        private async Task<ApiResource> GetApiResource(string name)
        {
            var query = await _scopeQuery.Get(new GetScopeByNameRequest
            {
                Name = name
            });

            return new ApiResource(query.Scope.Name);
        }
    }
}
