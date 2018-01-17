using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HMRC.ESFA.Levy.Api.Stub.Data;
using HMRC.ESFA.Levy.Api.Stub.StubbedObjects;
using HMRC.ESFA.Levy.Api.Types;

namespace HMRC.ESFA.Levy.Api.Stub.Controllers
{
    public class EmployerChecksController : ApiController
    {
        private string ConnectionString;
        private InertLogger Logger;
        private EmployerChecksRepository Repository;

        public EmployerChecksController()
        {
            ConnectionString = ConfigurationManager.AppSettings["DataConnectionString"];
            Logger = new InertLogger();
            Repository = new EmployerChecksRepository(ConnectionString, Logger);
        }

        [HttpGet]
        [Route("apprenticeship-levy/epaye/{empRef}/employed/{nino}")]
        public async Task<EmploymentStatus> GetEmploymentStatus(string empRef, string nino, DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            empRef = HttpUtility.UrlDecode(empRef);

            return await Repository.GetEmploymentStatus(empRef, nino, fromDate, toDate);
        }
    }
}