using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public interface IGatewayRepository
    {
        Task<IEnumerable<GatewayUser>> GetGatewayRecordsForId(string id);
    }
}
