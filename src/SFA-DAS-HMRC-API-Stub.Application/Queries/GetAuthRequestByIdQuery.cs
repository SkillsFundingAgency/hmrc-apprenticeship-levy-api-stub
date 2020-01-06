using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetAuthRequestByIdQuery : IQuery<GetAuthRequestByIdRequest, GetAuthRequestByIdResponse>
    {
        private readonly IAuthRequestRepository _authRequestRepository;

        public GetAuthRequestByIdQuery(IAuthRequestRepository authRequestRepository)
        {
            _authRequestRepository = authRequestRepository ?? throw new ArgumentException("authRequestRepository cannot be null");
        }

        public async Task<GetAuthRequestByIdResponse> Get(GetAuthRequestByIdRequest request)
        {
            var result = await _authRequestRepository.GetAuthRequestById(request.AuthId);

            if(result == null)
            {
                return null;
            }

            return new GetAuthRequestByIdResponse
            {
                AuthRequest = result
            };
        }
    }
}
