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

       
        [HttpGet]
        [Route("{empRef1}/{empRef2}/employed/{nino}")]
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
#pragma warning disable S3358 // Ternary operators should not be nested
                return BadRequest($"Missing parameter: {(!fromDate.HasValue ? "fromDate" : !toDate.HasValue ? "toDate" : string.Empty)}");
#pragma warning restore S3358 // Ternary operators should not be nested
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