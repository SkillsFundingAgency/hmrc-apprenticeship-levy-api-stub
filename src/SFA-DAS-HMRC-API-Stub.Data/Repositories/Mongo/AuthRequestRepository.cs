using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public class AuthRequestRepository : BaseRepository, IAuthRequestRepository
    {
        private readonly ILogger<AuthRequestRepository> _logger;
        private readonly IMongoCollection<AuthRequest> _authRequest;

        public AuthRequestRepository(IMongoDatabase database, ILogger<AuthRequestRepository> logger)
            :base(database)
        {
            _logger = logger ?? throw new ArgumentException("logger cannot be null");
            _authRequest = Database.GetCollection<AuthRequest>("sys_auth_requests");
        }

        public async Task<bool> DeleteAuthRequestById(long id)
        {
            var result = await _authRequest.DeleteOneAsync(ar => ar.Id == id);

            return result.IsAcknowledged && result.DeletedCount == 1;
        }

        public async Task<AuthRequest> GetAuthRequestById(long id)
        {
            var result = await _authRequest
                .FindAsync(ar => ar.Id == id)
            ;

            return result.FirstOrDefault();
        }

        public async Task Insert(AuthRequest authRequest)
        {
            await _authRequest.InsertOneAsync(authRequest);
        }
    }
}
