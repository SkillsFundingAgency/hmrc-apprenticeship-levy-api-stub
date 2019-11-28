using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;

namespace SFA.DAS.HMRC.API.Stub.Controllers
{
    [ApiController]
    public class EmployerChecksController : ControllerBase
    {
        private readonly ICommand<GetEmployerChecksRequest, GetEmployerChecksResponse> _getEmployerChecksCommand;
        private readonly ILogger<EmployerChecksController> _logger;

        public EmployerChecksController(
            ICommand<GetEmployerChecksRequest, GetEmployerChecksResponse> getEmployerChecksCommand,
            ILogger<EmployerChecksController> logger)
        {
            _getEmployerChecksCommand = getEmployerChecksCommand;
            _logger = logger;
        }

        [HttpGet]
        [Route("apprenticeship-levy/epaye/{empRef1}/{empRef2}/employed/{nino}")]
        public async Task<IActionResult> GetEmploymentStatus(
            string empRef1,
            string empRef2,
            string nino,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            _logger.LogDebug("Start GetEmploymentStatus action");

            if (fromDate == null || toDate == null)
            {
                return BadRequest($"Missing parameter: {(!fromDate.HasValue ? "fromDate" : !toDate.HasValue ? "toDate" : string.Empty)}");
            }

            var result = await _getEmployerChecksCommand.Get(new GetEmployerChecksRequest($"{empRef1}/{empRef2}", nino, fromDate, toDate));

            _logger.LogDebug("End GetEmploymentStatus action");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}