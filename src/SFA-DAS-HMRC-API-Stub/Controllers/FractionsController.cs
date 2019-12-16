using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Filters;

namespace SFA_DAS_HMRC_API_Stub.Controllers
{
    [TypeFilter(typeof(AuthorisationFilter))]
    [Route("apprenticeship-levy/epaye")]
    [ApiController]
    public class FractionsController : ControllerBase
    {
        private readonly ICommand<GetFractionsRequest, GetFractionsResponse> _getFractionsCommand;
        private readonly ICommand<GetFractionCalDateRequest, GetFractionCalDateResponse> _getFractionsCalDateCommand;
        private readonly ILogger<FractionsController> _logger;


        public FractionsController(
            ICommand<GetFractionsRequest, GetFractionsResponse> getFractionsCommand,
            ICommand<GetFractionCalDateRequest, GetFractionCalDateResponse> getFractionCalDateCommand,
            ILogger<FractionsController> logger)
        {
            _getFractionsCommand = getFractionsCommand;
            _getFractionsCalDateCommand = getFractionCalDateCommand;
            _logger = logger;
        }

        [HttpGet]
        [Route("{empRef1}/{empRef2}/fractions")]
        public async Task<IActionResult> GetFractionsData(
            string empRef1,
            string empRef2,
            DateTime fromDate,
            DateTime toDate)
        {
            _logger.LogDebug("Start FractionsData action");

            var result = await _getFractionsCommand.Get(new GetFractionsRequest($"{empRef1}/{empRef2}", fromDate, toDate));

            _logger.LogDebug("End FractionsData action");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Fraction);
        }

        [HttpGet]
        [Route("{empRef1}/{empRef2}/fractions-calculation-date")]
        public async Task<IActionResult> GetFractionCalculationDate(
            DateTime lastCalculationDate)
        {
            _logger.LogDebug("start fractionCalculationDate action");

            var result = await _getFractionsCalDateCommand.Get(new GetFractionCalDateRequest(lastCalculationDate));

            _logger.LogDebug("End fractionCalculationDate action");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.LastCalculationDate);
        }
    }
}