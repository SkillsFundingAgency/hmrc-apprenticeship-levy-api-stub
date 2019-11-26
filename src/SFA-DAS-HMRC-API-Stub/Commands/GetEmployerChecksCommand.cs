using SFA.DAS.HMRC.API.Stub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetEmployerChecksCommand : ICommand<GetEmployerChecksRequest, GetEmployerChecksResponse>
    {
        private readonly IEmployerChecksRepository _employerChecksRepository;

        public GetEmployerChecksCommand(IEmployerChecksRepository employerChecksRepository)
        {
            _employerChecksRepository = employerChecksRepository ?? throw new ArgumentException("employerChecksRepository cannot be null");
        }

        public async Task<GetEmployerChecksResponse> Get(GetEmployerChecksRequest request)
        {
            var result =  await _employerChecksRepository.GetEmploymentStatus(request.EmpRef, request.Nino);

            if(result == null)
            {
                return null;
            }

            return new GetEmployerChecksResponse
            {
                Empref = result.EmpRef,
                Nino = result.Nino,
                Employed = result.Employed,
                FromDate = result.FromDate.Value,
                ToDate = result.ToDate.Value
            };
        }
    }
}
