using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.HMRC.API.Stub.Commands;

namespace SFA.DAS.HMRC.API.Stub.Controllers
{
    public class EmployerChecksController : ControllerBase
    {
        private readonly ICommand<GetEmployerChecksRequest, GetEmployerChecksResponse> GetEmployerChecksCommand;

        public EmployerChecksController(ICommand<GetEmployerChecksRequest, GetEmployerChecksResponse> getEmployerChecksCommand)
        {
            GetEmployerChecksCommand = getEmployerChecksCommand;
        }

        [HttpGet]
        [Route("apprenticeship-levy/epaye/{empRef1}/{empRef2}/{nino}")]
        public async Task<IActionResult> GetEmploymentStatus(
            string empRef1,
            string empRef2,
            string nino,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            if (fromDate == null || toDate == null)
            {
                return BadRequest();
            }

            var result = await GetEmployerChecksCommand.Get(new GetEmployerChecksRequest($"{empRef1}/{empRef2}", nino, fromDate, toDate));

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}