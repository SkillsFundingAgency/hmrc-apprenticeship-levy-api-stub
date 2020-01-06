using SFA.DAS.HMRC.API.Stub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetEmployerChecksQuery : IQuery<GetEmployerChecksRequest, GetEmployerChecksResponse>
    {
        private readonly IEmployerChecksRepository _employerChecksRepository;

        public GetEmployerChecksQuery(IEmployerChecksRepository employerChecksRepository)
        {
            _employerChecksRepository = employerChecksRepository ?? throw new ArgumentException("employerChecksRepository cannot be null");
        }

        public async Task<GetEmployerChecksResponse> Get(GetEmployerChecksRequest request)
        {
            var result =  await _employerChecksRepository.GetEmploymentStatus(request.EmpRef, request.Nino, request.FromDate, request.ToDate);

            if(result == null)
            {
                return null;
            }

            return new GetEmployerChecksResponse
            {
                Empref = result.EmpRef,
                Nino = result.Nino,
                Employed = (request.FromDate.Value.Date >= result.FromDate && request.ToDate.Value.Date < result.ToDate),
                FromDate = request.FromDate,
                ToDate = request.ToDate
            };
        }
    }
}
