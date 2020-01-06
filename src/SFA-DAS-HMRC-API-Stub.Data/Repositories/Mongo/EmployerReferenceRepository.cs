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
    public class EmployerReferenceRepository : BaseRepository, IEmployerReferenceRepository
    {
        private readonly ILogger<EmployerReferenceRepository> _logger;
        private readonly IMongoCollection<EmployerReference> _employerReference;

        public EmployerReferenceRepository(IMongoDatabase database, ILogger<EmployerReferenceRepository> logger)
            : base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _employerReference = Database.GetCollection<EmployerReference>("emprefs");
        }

        public async Task<EmployerReference> GetEmployerReference(string empRef)
        {
            _logger.LogDebug($"GetEmployerReference: {empRef}");

            return _employerReference
                .Find(erf => erf.EmpRef == empRef)
                .ToList()
                .SingleOrDefault()
            ;
        }
    }
}
