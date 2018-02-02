using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HMRC.ESFA.Levy.Api.Stub.Api;
using HMRC.ESFA.Levy.Api.Stub.Data;
using HMRC.ESFA.Levy.Api.Stub.StubbedObjects;
using HMRC.ESFA.Levy.Api.Types;

namespace HMRC.ESFA.Levy.Api.Stub.Controllers
{
    public class EmployerChecksController : ApiController
    {
        private readonly IStubApi _stubApi;

        public EmployerChecksController()
        {
            var connectionString = ConfigurationManager.AppSettings["DataConnectionString"];

            _stubApi = new StubApi(new EmployerChecksRepository(connectionString, new InertLogger()));
        }

        public EmployerChecksController(IStubApi stubApi)
        {
            _stubApi = stubApi;
        }

        [HttpGet]
        [Route("apprenticeship-levy/epaye/{*parameters}")]
        public async Task<EmploymentStatus> GetEmploymentStatus(string parameters, DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if(fromDate == null || toDate == null)
            {
                RespondWithHttpStatus(400);
                return null;
            }

            var paramParts = parameters.Split('/');
            var empRef = paramParts[0] + "/" + paramParts[1];
            var nino = paramParts[3];

            var result = await _stubApi.GetEmploymentStatus(empRef, nino, fromDate, toDate);

            if (result.HttpStatusCode == null) return result;

            RespondWithHttpStatus(result.HttpStatusCode.Value);
            return null;
        }

        private void RespondWithHttpStatus(int httpStatusCode)
        {
            HttpContext.Current.Response.StatusCode = httpStatusCode;
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}