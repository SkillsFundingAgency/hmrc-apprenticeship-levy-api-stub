using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetEmployerReferenceQuery : IQuery<GetEmployerReferenceRequest, GetEmployerrReferenceResponse>
    {
        private readonly IEmployerReferenceRepository _employerReferenceRepository;

        public GetEmployerReferenceQuery(IEmployerReferenceRepository employerReferenceRepository)
        {
            _employerReferenceRepository = employerReferenceRepository ?? throw new ArgumentException("employerReferenceRepository cannot be null");
        }

        public async Task<GetEmployerrReferenceResponse> Get(GetEmployerReferenceRequest request)
        {
            var result =  await _employerReferenceRepository.GetEmployerReference(request.EmpRef);

            if(result == null)
            {
                return null;
            }

            return new GetEmployerrReferenceResponse
            {
                EmployerReference = result
            };
        }
    }
}
