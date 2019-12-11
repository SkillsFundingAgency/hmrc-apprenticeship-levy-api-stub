using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class AuthRecordCosmosRepository : IAuthRecordRepository
    {
        private readonly DocumentClient _client;
        private readonly ILogger<AuthRecordCosmosRepository> _logger;
        private readonly Uri _collectionUri;

        public AuthRecordCosmosRepository(DocumentClient client, ILogger<AuthRecordCosmosRepository> logger, Uri collectionUri)
        {
            _client = client;
            _logger = logger;
            _collectionUri = collectionUri;
        }

        public async Task<IEnumerable<AuthRecord>> GetAuthRecords(string token)
        {
            _logger.LogDebug($"Getting auth records, empRef: {token}");

            var query = _client.CreateDocumentQuery<AuthRecord>(_collectionUri)
               .Where(auth => auth.AccessToken == token)
               .ToList()
            ;

            // Can we push this into the query?
            return query.Where(q => q.RefreshedAt.AddMilliseconds(q.ExpiresIn * 1000) >= DateTime.UtcNow);
        }
    }
}
