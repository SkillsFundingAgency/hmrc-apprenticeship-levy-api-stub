using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public class ScopeRepository : BaseRepository, IScopeRepository
    {
        private readonly ILogger<ScopeRepository> _logger;
        private readonly IMongoCollection<Scope> _scopes;

        public ScopeRepository(IMongoDatabase database, ILogger<ScopeRepository> logger)
            : base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _scopes = Database.GetCollection<Scope>("sys_scopes");
        }

        public async Task<IEnumerable<Scope>> GetAllScopes()
        {
            var result = await _scopes.FindAsync(s => true);

            return result.ToList();
        }

        public async Task<IEnumerable<Scope>> GetScopeByName(string name)
        {
            _logger.LogInformation($"Getting scope for name {name}");

            return _scopes
                .Find(scope => scope.Name == name)
                .ToList()
            ;
        }
    }
}
