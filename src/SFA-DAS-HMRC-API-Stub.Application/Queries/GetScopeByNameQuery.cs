using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetScopeByNameQuery : IQuery<GetScopeByNameRequest, GetScopeByNameResponse>
    {
        private readonly IScopeRepository _scopeRepository;

        public GetScopeByNameQuery(IScopeRepository scopeRepository)
        {
            _scopeRepository = scopeRepository;
        }

        public async Task<GetScopeByNameResponse> Get(GetScopeByNameRequest request)
        {
            var query = await _scopeRepository.GetScopeByName(request.Name);

            if(query == null)
            {
                return null;
            }

            return new GetScopeByNameResponse()
            {
                Scope = query.First()
            };
        }
    }
}
