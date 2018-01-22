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
        [Route("apprenticeship-levy/epaye/{*parameters}")]
        public async Task<EmploymentStatus> GetEmploymentStatus(string parameters, DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            var paramParts = parameters.Split('/');
            var empRef = paramParts[0] + "/" + paramParts[1];
            string nino = paramParts[3];
            return await Repository.GetEmploymentStatus(empRef, nino, fromDate, toDate);
        }
    }
}