using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class GatewayCosmosRepository : BaseCosmosRepository, IGatewayRepository
    {
        private readonly ILogger<GatewayCosmosRepository> _logger;

        public GatewayCosmosRepository(IDocumentClient client, ILogger<GatewayCosmosRepository> logger, Uri collectionUri)
            : base(client, collectionUri)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
        }

        public async Task<IEnumerable<GatewayUser>> GetGatewayRecordsForId(string gatewayId)
        {
            return Client.CreateDocumentQuery<GatewayUser>(CollectionUri)
                .Where(gw => gw.GatewayId == gatewayId)
                .ToList()
            ;
        }
    }
}
