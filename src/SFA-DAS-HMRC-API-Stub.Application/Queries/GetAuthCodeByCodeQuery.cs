using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetAuthCodeByCodeQuery : IQuery<GetAuthCodeByCodeRequest, GetAuthCodeByCodeResponse>
    {
        private readonly IAuthCodeRepository _authCodeRepository;

        public GetAuthCodeByCodeQuery(IAuthCodeRepository authCodeRepository)
        {
            _authCodeRepository = authCodeRepository;
        }

        public async Task<GetAuthCodeByCodeResponse> Get(GetAuthCodeByCodeRequest request)
        {
            var query = await _authCodeRepository.GetByCode(request.Code);

            if (query == null)
            {
                return null;
            }

            return new GetAuthCodeByCodeResponse
            {
                AuthCode = query
            };
        }
    }
}
