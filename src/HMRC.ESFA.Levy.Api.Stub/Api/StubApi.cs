using System;
using System.Threading.Tasks;
using HMRC.ESFA.Levy.Api.Stub.Data;
using HMRC.ESFA.Levy.Api.Stub.Models;
using HMRC.ESFA.Levy.Api.Stub.Repositorys;

namespace HMRC.ESFA.Levy.Api.Stub.Api
{
    public class StubApi : IStubApi
    {
        private IEmployerChecksRepository _employerChecksRepository;

        public StubApi(IEmployerChecksRepository employerChecksRepository)
        {
            _employerChecksRepository = employerChecksRepository;
        }

        public async Task<EmployerStatusExtended> GetEmploymentStatus(string empRef, string nino, DateTime? fromDate,
            DateTime? toDate = null)
        {
            try
            {
                EmployerStatusExtended result;

                if (fromDate.HasValue)
                {
                    result = await _employerChecksRepository.GetEmploymentStatusInDateRange(empRef, nino, fromDate, toDate);
                }
                else
                {
                    result = await _employerChecksRepository.GetEmploymentStatus(empRef, nino);
                }

                return result ?? (new EmployerStatusExtended
                {
                    Employed = false,
                    Empref = empRef,
                    Nino = nino,
                });
            }
            catch (Exception e)
            {
                return new EmployerStatusExtended
                {
                    Exception = e
                };
            }
        }

    }
}