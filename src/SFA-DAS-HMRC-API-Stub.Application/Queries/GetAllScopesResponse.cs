using SFA.DAS.HMRC.API.Stub.Domain;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetAllScopesResponse
    {
        public IEnumerable<Scope> Scopes { get; set; }
    }
}