using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class AuthRecordCosmosRepository : BaseCosmosRepository, IAuthRecordRepository
    {
        private readonly ILogger<AuthRecordCosmosRepository> _logger;

        public AuthRecordCosmosRepository(DocumentClient client, ILogger<AuthRecordCosmosRepository> logger, Uri collectionUri)
            :base(client, collectionUri)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
        }

        public async Task<IEnumerable<AuthRecord>> GetAuthRecords(string token)
        {
            _logger.LogDebug($"Getting auth records, empRef: {token}");

            var query = Client.CreateDocumentQuery<AuthRecord>(CollectionUri)
               .Where(auth => auth.AccessToken == token)
               .ToList()
            ;

            // Can we push this into the query?
            return query.Where(q => q.RefreshedAt.AddMilliseconds(q.ExpiresIn * 1000) >= DateTime.UtcNow);
        }
    }
}
