using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetEmployerReferenceCommand : ICommand<GetEmployerReferenceRequest, GetEmployerrReferenceResponse>
    {
        private readonly IEmployerReferenceRepository _employerReferenceRepository;

        public GetEmployerReferenceCommand(IEmployerReferenceRepository employerReferenceRepository)
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
