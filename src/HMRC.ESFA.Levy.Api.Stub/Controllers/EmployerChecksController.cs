using System;
using System.Web.Http;
using HMRC.ESFA.Levy.Api.Types;

namespace HMRC.ESFA.Levy.Api.Stub.Controllers
{
    public class EmployerChecksController : ApiController
    {
        // GET api/<controller>
        public EmploymentStatus GetEmploymentStatus(string authToken, string empRef, string nino,
            DateTime? fromDate = null, DateTime? toDate = null)
        {
            return FetchEmploymentStatus(empRef);
        }

        // GET api/<controller>
        public EmploymentStatus GetEmploymentStatus(string empRef, string nino, DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            return FetchEmploymentStatus(empRef);

        }

        private static EmploymentStatus FetchEmploymentStatus(string empRef)
        {
            return new EmploymentStatus
            {
                Employed = true,
                Empref = empRef,
                FromDate = DateTime.MinValue,
                ToDate = DateTime.MaxValue,
                Nino = "NinoString"
            };
        }
    }
}