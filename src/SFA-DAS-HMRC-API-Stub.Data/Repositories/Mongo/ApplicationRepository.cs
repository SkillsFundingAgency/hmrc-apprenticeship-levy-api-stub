using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public class ApplicationRepository : BaseRepository, IApplicationRepository
    {
        private readonly ILogger<ApplicationRepository> _logger;
        private readonly IMongoCollection<Application> _applications;

        public ApplicationRepository(IMongoDatabase database, ILogger<ApplicationRepository> logger)
            : base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _applications = Database.GetCollection<Application>("applications");
        }

        public async Task<IEnumerable<Application>> GetApplicationByClientId(string clientId)
        {
            _logger.LogInformation($"Getting application for ClientId {clientId}");

            return _applications
                .Find(a => a.ClientID == clientId)
                .ToList()
            ;
        }
    }
}
