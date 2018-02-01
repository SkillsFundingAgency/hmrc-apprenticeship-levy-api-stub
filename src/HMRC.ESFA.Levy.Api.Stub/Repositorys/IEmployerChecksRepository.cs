using System;
using System.Threading.Tasks;
using HMRC.ESFA.Levy.Api.Stub.Models;

namespace HMRC.ESFA.Levy.Api.Stub.Repositorys
{
    public interface IEmployerChecksRepository
    {
        Task<EmployerStatusExtended> GetEmploymentStatus(string empRef, string nino);
        Task<EmployerStatusExtended> GetEmploymentStatusInDateRange(string empRef, string nino, DateTime? fromDate = null, DateTime? toDate = null);
    }
}