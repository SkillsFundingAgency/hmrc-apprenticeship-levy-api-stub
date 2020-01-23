using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetAuthRecordtByRefreshTokenQuery : IQuery<GetAuthRecordtByRefreshTokenRequest, GetAuthRecordtByRefreshTokenResponse>
    {
        private readonly IAuthRecordRepository _authRecordRepository;

        public GetAuthRecordtByRefreshTokenQuery(IAuthRecordRepository authRecordRepository)
        {
            _authRecordRepository = authRecordRepository ?? throw new ArgumentException("authRecordRepository cannot be null");
        }

        public async Task<GetAuthRecordtByRefreshTokenResponse> Get(GetAuthRecordtByRefreshTokenRequest request)
        {
            var result = await _authRecordRepository.GetAuthRecordsByRefreshToken(request.RefreshToken);

            if(result == null)
            {
                return null;
            }

            return new GetAuthRecordtByRefreshTokenResponse
            {
                AuthRecord = result.FirstOrDefault()
            };
        }
    }
}
