using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Services
{
    public class AuthenticationService : IAuthenticate
    {
        private readonly IAuthRecordRepository _authRecordRepository;
        private readonly IGatewayRepository _gatewayRepository;

        public AuthenticationService(
            IAuthRecordRepository authRecordRepository,
            IGatewayRepository gatewayRepository 
        )
        {
            _authRecordRepository = authRecordRepository;
            _gatewayRepository = gatewayRepository;
        }

        public async Task<AuthResponse> IsAuthenticated(string token)
        {
            var authRecords = await _authRecordRepository.GetAuthRecords(token);

            if(authRecords.Any(au => au.IsPrivileged))
            {
                return new AuthResponse()
                {
                    IsAuthenticated = true,
                    IsPrivileged = true
                };
            }

            if (authRecords.Any(au => !au.IsPrivileged))
            {
                return new AuthResponse()
                {
                    IsAuthenticated = true,
                    IsPrivileged = false,
                    GatewayId = authRecords.First().GatewayId
                };
            }

            return new AuthResponse();
        }

        public async Task<bool> IsAuthorized(string gatewayId, string empRef)
        {
            if (string.IsNullOrWhiteSpace(gatewayId) || string.IsNullOrWhiteSpace(empRef))
            {
                return false;
            }

            var gatewayUsers = await _gatewayRepository.GetGatewayRecordsForId(gatewayId);
            return gatewayUsers.Any(u => u.EmpRef == empRef);
        }
    }
}
