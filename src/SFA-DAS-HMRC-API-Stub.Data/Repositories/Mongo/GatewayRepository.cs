using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public class GatewayRepository : BaseRepository, IGatewayRepository
    {
        private readonly ILogger<GatewayRepository> _logger;
        private readonly IMongoCollection<GatewayUser> _gatewayUser;

        public GatewayRepository(IMongoDatabase database, ILogger<GatewayRepository> logger)
            : base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _gatewayUser = Database.GetCollection<GatewayUser>("gateway_users");
        }

        public async Task<IEnumerable<GatewayUser>> GetGatewayRecordsForId(string gatewayId)
        {
            return _gatewayUser
                .Find(gw => gw.GatewayId == gatewayId)
                .ToList()
            ;
        }

        public async Task<GatewayUser> GetGatewayRecordsByIdAndPassword(string gatewayId, string password)
        {
            var result = await _gatewayUser
                .FindAsync(gw => gw.GatewayId == gatewayId && gw.Password == password)
            ;

            return result.FirstOrDefault();
        }
    }
}
