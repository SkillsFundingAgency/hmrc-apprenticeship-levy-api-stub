using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetAllScopesQuery : IQuery<GetAllScopesRequest, GetAllScopesResponse>
    {
        private readonly IScopeRepository _scopeRepository;

        public GetAllScopesQuery(IScopeRepository scopeRepository)
        {
            _scopeRepository = scopeRepository;
        }

        public async Task<GetAllScopesResponse> Get(GetAllScopesRequest request)
        {
            var query = await _scopeRepository.GetAllScopes();

            if(query == null)
            {
                return null;
            }

            return new GetAllScopesResponse()
            {
                Scopes = query
            };
        }
    }
}
