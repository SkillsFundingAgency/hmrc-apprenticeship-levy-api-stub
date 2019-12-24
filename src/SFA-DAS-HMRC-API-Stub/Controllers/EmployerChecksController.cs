using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Filters;

namespace SFA.DAS.HMRC.API.Stub.Controllers
{
    [TypeFilter(typeof(AuthorisationFilter))]
    [Route("apprenticeship-levy/epaye")]
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


        /// <summary>
        /// Returns the employment status for the given employer between the given dates
        /// </summary>
        /// <param name="empRef1">The first part of the employer reference</param>
        /// <param name="empRef2">The second part of the employer reference</param>
        /// <param name="nino">The employers nino</param>
        /// <param name="fromDate">The from date</param>
        /// <param name="toDate">The to date</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{empRef1}/{empRef2}/employed/{nino}")]  
        public async Task<IActionResult> GetEmploymentStatus(
            string empRef1,
            string empRef2,
            string nino,
            DateTime fromDate,
            DateTime toDate)
        {
            _logger.LogDebug("Start GetEmploymentStatus action");

            if (fromDate > toDate)
            {
#pragma warning disable S3358 // Ternary operators should not be nested
                return BadRequest(new { code = "BAD_DATE_RANGE", message = "The fromDate cannot be before the toDate" });
#pragma warning restore S3358 // Ternary operators should not be nested
            }

            var result = await _getEmployerChecksCommand.Get(new GetEmployerChecksRequest($"{empRef1}/{empRef2}", nino, fromDate, toDate));

            _logger.LogDebug("End GetEmploymentStatus action");

            if (result == null)
            {
                return NotFound(new { code = "EPAYE_UNKNOWN", message = "The provided NINO or EMPREF was not recognised" });
            }

            return Ok(result);
        }
    }
}