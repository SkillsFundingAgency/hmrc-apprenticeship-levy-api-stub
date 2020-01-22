using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetApplicationsByIdQuery : IQuery<GetApplicationByIdRequest, GetApplicationByIdResponse>
    {
        private readonly IApplicationRepository _applicationRepository;

        public GetApplicationsByIdQuery(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<GetApplicationByIdResponse> Get(GetApplicationByIdRequest request)
        {
            var query = await _applicationRepository.GetApplicationByClientId(request.ClientId);

            if(query == null || !query.Any())
            {
                return null;
            }

            return new GetApplicationByIdResponse()
            {
                Application = query.First()
            };
        }
    }
}
