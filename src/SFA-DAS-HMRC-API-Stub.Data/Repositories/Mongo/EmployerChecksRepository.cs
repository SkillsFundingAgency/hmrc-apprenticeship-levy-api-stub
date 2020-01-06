using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Domain;
using SFA.DAS.HMRC.API.Stub.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public class EmployerChecksRepository : BaseRepository, IEmployerChecksRepository
    {
        private readonly ILogger<EmployerChecksRepository> _logger;
        private readonly IMongoCollection<EmployerStatus> _employerStatus;

        public EmployerChecksRepository(IMongoDatabase database, ILogger<EmployerChecksRepository> logger)
            : base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _employerStatus = Database.GetCollection<EmployerStatus>("employments");
        }

        public async Task<EmployerStatus> GetEmploymentStatus(string empRef, string nino, DateTime? fromDate = null, DateTime? toDate = null)
        {
            _logger.LogDebug($"Getting employment status in date range, empRef: {empRef}, nino: {nino}, fromDate: {fromDate}, toDate: {toDate}");

            var query = _employerStatus.Find(es => es.EmpRef == empRef && es.Nino == nino)
            ;

            return query
                .ToList()
                .FirstOrDefault()
            ;
        }
    }
}
