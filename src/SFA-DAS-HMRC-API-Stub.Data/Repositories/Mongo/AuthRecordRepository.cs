using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public class AuthRecordRepository : BaseRepository, IAuthRecordRepository
    {
        private readonly ILogger<AuthRecordRepository> _logger;
        private readonly IMongoCollection<AuthRecord> _authRecords;

        public AuthRecordRepository(IMongoDatabase database, ILogger<AuthRecordRepository> logger)
            : base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _authRecords = Database.GetCollection<AuthRecord>("sys_auth_records");
        }

        public async Task<IEnumerable<AuthRecord>> GetAuthRecords(string token)
        {
            _logger.LogDebug($"Getting auth records, empRef: {token}");

            var query = _authRecords
               .Find(auth => auth.AccessToken == token)
               .ToList()
            ;

#if DEBUG
            return query;
#endif
            // Can we push this into the query?
            //TODO: Check logic
            return query.Where(q => q.RefreshedAt != null
                //? DateTime.Parse(q.RefreshedAt.GetElement("$date").Value.ToString()).AddMilliseconds(q.ExpiresIn * 1000) >= DateTime.UtcNow
                //: DateTime.Parse(q.CreatedAt.GetElement("$date").Value.ToString()).AddMilliseconds(q.ExpiresIn * 1000) >= DateTime.UtcNow);
                //? q.RefreshedAt.AddMilliseconds(q.ExpiresIn * 1000) >= DateTime.UtcNow
                //: q.CreatedAt.AddMilliseconds(q.ExpiresIn * 1000) >= DateTime.UtcNow);
                ? q.RefreshedAt.ToUniversalTime().AddMilliseconds(q.ExpiresIn * 1000) >= DateTime.UtcNow
                : q.CreatedAt.ToUniversalTime().AddMilliseconds(q.ExpiresIn * 1000) >= DateTime.UtcNow);
        }


        public async Task<IEnumerable<AuthRecord>> GetAuthRecordsByRefreshToken(string refreshToken)
        {
            _logger.LogDebug($"Getting auth records, refresh token: {refreshToken}");

            var query = _authRecords
               .Find(auth => auth.RefreshToken == refreshToken)
               .ToList()
            ;

            return query;
        }

        public async Task Insert(AuthRecord authRecord)
        {
            await _authRecords.InsertOneAsync(authRecord);
        }

        public async Task Update(string refreshToken, AuthRecord updatedAuthRecord)
        {
            var filter = Builders<AuthRecord>.Filter.Eq("refreshToken", refreshToken);
            var update = Builders<AuthRecord>.Update
                .Set("refreshToken", updatedAuthRecord.RefreshToken)
                .Set("accessToken", updatedAuthRecord.AccessToken)
                .Set("refreshedAt", updatedAuthRecord.RefreshedAt);
            
            await _authRecords.UpdateOneAsync(filter, update);
        }
    }
}
