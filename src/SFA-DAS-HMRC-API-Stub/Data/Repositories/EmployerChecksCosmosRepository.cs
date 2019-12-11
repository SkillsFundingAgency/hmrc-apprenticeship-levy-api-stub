using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Domain;
using SFA.DAS.HMRC.API.Stub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class EmployerChecksCosmosRepository : IEmployerChecksRepository
    {
        private readonly DocumentClient _client;
        private readonly ILogger<EmployerChecksCosmosRepository> _logger;
        private readonly Uri _collectionUri;

        public EmployerChecksCosmosRepository(DocumentClient client, ILogger<EmployerChecksCosmosRepository> logger, Uri collectionUri)
        {
            _client = client;
            _logger = logger;
            _collectionUri = collectionUri;
        }

        public async Task<EmployerStatus> GetEmploymentStatus(string empRef, string nino, DateTime? fromDate = null, DateTime? toDate = null)
        {
            _logger.LogDebug($"Getting employment status in date range, empRef: {empRef}, nino: {nino}, fromDate: {fromDate}, toDate: {toDate}");

            var query = _client.CreateDocumentQuery<EmployerStatus>(_collectionUri, new FeedOptions() { MaxItemCount = 1 })
               .Where(es => es.EmpRef == empRef && es.Nino == nino)
               .Where(es => fromDate.Value.Date >= es.FromDate && toDate.Value.Date < es.ToDate)
            ;

            return query
                .ToList()
                .FirstOrDefault()
            ;
        }
    }
}
