using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public class AuthCodeRepository : BaseRepository, IAuthCodeRepository
    {
        private readonly ILogger<AuthCodeRepository> _logger;
        private readonly IMongoCollection<AuthCode> _authCodes;

        public AuthCodeRepository(IMongoDatabase database, ILogger<AuthCodeRepository> logger)
            :base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _authCodes = Database.GetCollection<AuthCode>("sys_auth_codes");
        }

        public async Task<bool> DeleteByCode(string code)
        {
            var result = await _authCodes.DeleteOneAsync(ac => ac.AuthorizationCode == code);

            return result.IsAcknowledged && result.DeletedCount == 1;
        }

        public async Task<AuthCode> GetByCode(string code)
        {
            var query = await _authCodes.FindAsync(ac => ac.AuthorizationCode == code);

            return query.FirstOrDefault();
        }

        public async Task Insert(AuthCode authCode)
        {
            await _authCodes.InsertOneAsync(authCode);
        }
    }
}
