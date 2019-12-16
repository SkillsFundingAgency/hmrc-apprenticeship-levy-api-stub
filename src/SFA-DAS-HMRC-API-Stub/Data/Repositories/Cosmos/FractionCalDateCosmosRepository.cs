using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class FractionCalDateCosmosRepository : BaseCosmosRepository, IFractionCalDateRepository
    {
        private readonly ILogger<FractionCalDateCosmosRepository> _logger;

        public FractionCalDateCosmosRepository(DocumentClient client, ILogger<FractionCalDateCosmosRepository> logger, Uri collectionUri)
            : base(client, collectionUri)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
        }

        public async Task<FractionCalculationDate> GetByLastCalDate(
            DateTime? lastCalculationDate)
        {
            _logger.LogDebug($"Getting levy declaration by, fromDate: {lastCalculationDate}");

            var query = Client.CreateDocumentQuery<FractionCalculationDate>(CollectionUri, new FeedOptions() { MaxItemCount = 1 })
                    //.Where(f => f.EmpRef == empRef)
                    //.AsEnumerable()
                    //.SelectMany(f => f.FractionCalculation.Where(fd => fd.CalculatedAt.Date >= fromDate.Date && fd.CalculatedAt.Date < toDate.Date)) // sorted in reverse?
                ;

            return query
                .ToList()
                .FirstOrDefault();
                
            //{
            //    LastCalculationDate = lastCalculationDate.ToList()
            //};
        }
    }
}
