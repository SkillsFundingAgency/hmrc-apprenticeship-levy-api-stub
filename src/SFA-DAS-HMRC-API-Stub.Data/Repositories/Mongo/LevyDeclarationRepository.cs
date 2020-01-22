using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public class LevyDeclarationRepository : BaseRepository, ILevyDeclarationRepository
    {
        private readonly ILogger<LevyDeclarationRepository> _logger;
        private readonly IMongoCollection<LevyDeclaration> _declarations;

        public LevyDeclarationRepository(IMongoDatabase database, ILogger<LevyDeclarationRepository> logger)
            : base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _declarations = Database.GetCollection<LevyDeclaration>("declarations");
        }

        public async Task<LevyDeclaration> GetByEmpRef(
             string empRef,
             DateTime? fromDate,
             DateTime? toDate)
        {
            _logger.LogDebug($"Getting levy declaration by, fromDate: {fromDate}, toDate: {toDate}, empRef: {empRef}");

            var declarations = _declarations
                .Find(ld => ld.EmpRef == empRef)
                .ToList()
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
                Declarations = declarations.OrderByDescending(d => d.Id).ToList()
            };
        }
    }
}
