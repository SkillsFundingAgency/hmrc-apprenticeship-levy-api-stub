using System;
using System.Configuration;
using System.Threading.Tasks;
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
        public async Task<EmploymentStatus> GetEmploymentStatus(string authToken, string empRef, string nino,
            DateTime? fromDate = null, DateTime? toDate = null)
        {
            return await Repository.GetEmploymentStatus(empRef);
        }

        [HttpGet]
        public async Task<EmploymentStatus> GetEmploymentStatus(string empRef, string nino, DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            return await Repository.GetEmploymentStatus(empRef);
        }
    }
}