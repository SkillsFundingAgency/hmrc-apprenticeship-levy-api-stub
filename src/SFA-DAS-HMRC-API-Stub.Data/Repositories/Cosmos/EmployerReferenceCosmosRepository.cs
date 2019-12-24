using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class EmployerReferenceCosmosRepository : BaseCosmosRepository, IEmployerReferenceRepository
    {
        private readonly ILogger<EmployerReferenceCosmosRepository> _logger;

        public EmployerReferenceCosmosRepository(
            IDocumentClient client,
            ILogger<EmployerReferenceCosmosRepository> logger,
            Uri collectionUri
            ) : base(client, collectionUri)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
        }

        public async Task<EmployerReference> GetEmployerReference(string empRef)
        {
            _logger.LogDebug($"GetEmployerReference: {empRef}");

            return Client.CreateDocumentQuery<EmployerReference>(CollectionUri)
                .Where(erf => erf.EmpRef == empRef)
                .ToList()
                .SingleOrDefault()
            ;
        }
    }
}
