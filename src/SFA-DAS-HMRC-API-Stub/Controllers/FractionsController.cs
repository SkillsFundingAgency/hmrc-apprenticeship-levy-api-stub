using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Filters;

namespace SFA_DAS_HMRC_API_Stub.Controllers
{    
    [Route("apprenticeship-levy")]
    [ApiController]
    public class FractionsController : ControllerBase
    {
        private readonly ICommand<GetFractionsRequest, GetFractionsResponse> _getFractionsCommand;
        private readonly ICommand<GetFractionCalcDateRequest, GetFractionCalcDateResponse> _getFractionsCalcDateCommand;
        private readonly ILogger<FractionsController> _logger;


        public FractionsController(
            ICommand<GetFractionsRequest, GetFractionsResponse> getFractionsCommand,
            ICommand<GetFractionCalcDateRequest, GetFractionCalcDateResponse> getFractionCalcDateCommand,
            ILogger<FractionsController> logger)
        {
            _getFractionsCommand = getFractionsCommand;
            _getFractionsCalcDateCommand = getFractionCalcDateCommand;
            _logger = logger;
        }

        [TypeFilter(typeof(AuthorisationFilter))]
        [HttpGet]
        [Route("epaye/{empRef1}/{empRef2}/fractions")]
        public async Task<IActionResult> GetFractions(
            string empRef1,
            string empRef2,
            DateTime fromDate,
            DateTime toDate)
        {
            _logger.LogDebug("Start GetFractions action");

            var result = await _getFractionsCommand.Get(new GetFractionsRequest($"{empRef1}/{empRef2}", fromDate, toDate));

            _logger.LogDebug("End GetFractions action");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Fraction);
        }

        [HttpGet]
        [Route("fraction-calculation-date")]
        public async Task<IActionResult> GetFractionCalculationDate()
        {
            _logger.LogDebug("start GetFractionCalculationDate action");

            var result = await _getFractionsCalcDateCommand.Get(new GetFractionCalcDateRequest());

            _logger.LogDebug("End GetFractionCalculationDate action");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.LastCalculationDate.ToString("yyyy-MM-dd"));
        }
    }
}