using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public interface IGatewayRepository
    {
        Task<IEnumerable<GatewayUser>> GetGatewayRecordsForId(string id);
    }
}
