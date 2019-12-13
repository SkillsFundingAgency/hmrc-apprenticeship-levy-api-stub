using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public class LevyDeclarationCosmosRepository : BaseCosmosRepository, ILevyDeclarationRepository
    {
        private readonly ILogger<LevyDeclarationCosmosRepository> _logger;

        public LevyDeclarationCosmosRepository(
            DocumentClient client,
            ILogger<LevyDeclarationCosmosRepository> logger,
            Uri collectionUri
            ) : base(client, collectionUri)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
        }

        public async Task<LevyDeclaration> GetByEmpRef(
             string empRef,
             DateTime? fromDate,
             DateTime? toDate)
        {
            _logger.LogDebug($"Getting levy declaration by, fromDate: {fromDate}, toDate: {toDate}, empRef: {empRef}");

            var declarations = Client.CreateDocumentQuery<LevyDeclaration>(CollectionUri, new FeedOptions() { MaxItemCount = 1 })
                .Where(ld => ld.EmpRef == empRef)
                .AsEnumerable()
                .SelectMany(ld =>
                {
                    if (fromDate.HasValue && toDate.HasValue)
                    {
                        return ld.Declarations.Where(d => d.SubmissionTime.Date >= fromDate.Value.Date && d.SubmissionTime.Date < toDate.Value.Date);
                    }

                    return ld.Declarations;
                })
            ;

            return new LevyDeclaration()
            {
                EmpRef = empRef,
                Declarations = declarations.ToList()
            };
        }
    }
}
