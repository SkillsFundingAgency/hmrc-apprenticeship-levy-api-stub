using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Cosmos
{
    public class FractionsCosmosRepository : BaseCosmosRepository, IFractionsRepository, IFractionsCalcDateRepository
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

        public async Task<RootObject> GetByEmpRef(string empRef, DateTime fromDate, DateTime toDate)
        {

            _logger.LogDebug($"Getting levy declaration by, fromDate: {fromDate}, toDate: {toDate}, empRef: {empRef}");

            var fractions = Client.CreateDocumentQuery<RootObject>(CollectionUri, new FeedOptions() { MaxItemCount = 1 })
                .Where(f => f.EmpRef == empRef)
                .AsEnumerable()
                .SelectMany(f => f.FractionCalculations
                .Where(fd => fd.CalculatedAt.Date >= fromDate.Date && fd.CalculatedAt.Date < toDate.Date))
            ;

            return new RootObject()
            {
                EmpRef = empRef,
                FractionCalculations = fractions.ToList()
            };
        }

        public async Task<FractionCalculationDate> GetLastCalcDate()
        {
            _logger.LogDebug("Getting lastCalculationDate");

            return Client.CreateDocumentQuery<FractionCalculationDate>(CollectionUri, new FeedOptions() { MaxItemCount = 1 })
                .ToList()
                .FirstOrDefault();
        }
    }
}
