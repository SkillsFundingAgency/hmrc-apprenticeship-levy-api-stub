using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Domain;
using SFA.DAS.HMRC.API.Stub.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class EmployerChecksCosmosRepository : BaseCosmosRepository, IEmployerChecksRepository
    {
        private readonly ILogger<EmployerChecksCosmosRepository> _logger;

        public EmployerChecksCosmosRepository(IDocumentClient client, ILogger<EmployerChecksCosmosRepository> logger, Uri collectionUri)
            : base(client, collectionUri)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null"); 
        }

        public async Task<EmployerStatus> GetEmploymentStatus(string empRef, string nino, DateTime? fromDate = null, DateTime? toDate = null)
        {
            _logger.LogDebug($"Getting employment status in date range, empRef: {empRef}, nino: {nino}, fromDate: {fromDate}, toDate: {toDate}");

            var query = Client.CreateDocumentQuery<EmployerStatus>(CollectionUri, new FeedOptions() { MaxItemCount = 1 })
               .Where(es => es.EmpRef == empRef && es.Nino == nino)
            ;

            return query
                .ToList()
                .FirstOrDefault()
            ;
        }
    }
}
