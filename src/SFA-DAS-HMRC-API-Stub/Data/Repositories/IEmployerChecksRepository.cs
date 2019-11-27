using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Repositories
{
    public interface IEmployerChecksRepository
    {
       // Task<EmployerStatus> GetEmploymentStatus(string empRef, string nino);
        Task<EmployerStatus> GetEmploymentStatus(
            string empRef,
            string nino,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        ;
    }
}