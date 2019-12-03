using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class GatewayRepository : IGatewayRepository
    {
        private readonly IGatewayDataContext _gatewayDataContext;

        public GatewayRepository(IGatewayDataContext gatewayDataContext)
        {
            _gatewayDataContext = gatewayDataContext;
        }

        public async Task<IEnumerable<GatewayUser>> GetGatewayRecordsForId(string gatewayId)
        {
            return await _gatewayDataContext.GatewayUsers
                .Where(gw => gw.GatewayId == gatewayId)
                .ToListAsync();
        }
    }
}
