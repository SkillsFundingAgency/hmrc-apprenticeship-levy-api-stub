using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetAuthRecordtByAccessTokenQuery : IQuery<GetAuthRecordtByAccessTokenRequest, GetAuthRecordtByAccessTokenResponse>
    {
        private readonly IAuthRecordRepository _authRecordRepository;

        public GetAuthRecordtByAccessTokenQuery(IAuthRecordRepository authRecordRepository)
        {
            _authRecordRepository = authRecordRepository ?? throw new ArgumentException("authRecordRepository cannot be null");
        }

        public async Task<GetAuthRecordtByAccessTokenResponse> Get(GetAuthRecordtByAccessTokenRequest request)
        {
            var result = await _authRecordRepository.GetAuthRecords(request.AccessToken);

            if(result == null)
            {
                return null;
            }

            return new GetAuthRecordtByAccessTokenResponse
            {
                AuthRecord = result.FirstOrDefault()
            };
        }
    }
}
