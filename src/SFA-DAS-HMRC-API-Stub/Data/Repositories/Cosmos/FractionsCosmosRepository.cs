using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Cosmos
{
    public class FractionsCosmosRepository : BaseCosmosRepository, IFractionsRepository
    {
        private readonly ILogger<FractionsCosmosRepository> _logger;

        public FractionsCosmosRepository(
            DocumentClient client,
            ILogger<FractionsCosmosRepository> logger,
            Uri collectionUri
            ) : base(client, collectionUri)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
        }

        public async Task<FractionsData> GetByEmpRef(
            string empRef,
            DateTime fromDate,
            DateTime toDate)
        {

            _logger.LogDebug($"Getting levy declaration by, fromDate: {fromDate}, toDate: {toDate}, empRef: {empRef}");

            var fractions = Client.CreateDocumentQuery<FractionsData>(CollectionUri, new FeedOptions() { MaxItemCount = 1 })
                .Where(f => f.EmpRef == empRef)
                .AsEnumerable()
                .SelectMany(f => f.FractionCalculation.Where(fd => fd.CalculatedAt.Date >= fromDate.Date && fd.CalculatedAt.Date < toDate.Date)) // sorted in reverse?
            ;

            return new FractionsData()
            {
                EmpRef = empRef,
                FractionCalculation = fractions.ToList()
            };
        }
    }
}
