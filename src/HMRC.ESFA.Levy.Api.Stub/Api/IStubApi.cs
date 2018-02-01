using System;
using System.Threading.Tasks;
using HMRC.ESFA.Levy.Api.Stub.Models;

namespace HMRC.ESFA.Levy.Api.Stub.Api
{
    public interface IStubApi
    {
        Task<EmployerStatusExtended> GetEmploymentStatus(string empRef, string nino, DateTime? fromDate,
            DateTime? toDate = null);
    }
}