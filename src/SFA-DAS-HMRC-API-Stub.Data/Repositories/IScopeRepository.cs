using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public interface IScopeRepository
    {
        Task<IEnumerable<Scope>> GetScopeByName(string name);
        Task<IEnumerable<Scope>> GetAllScopes();
    }
}
