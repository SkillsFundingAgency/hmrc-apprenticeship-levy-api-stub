using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Application.Queries;

namespace SFA_DAS_HMRC_API_Stub.Controllers
{    
    [Route("apprenticeship-levy")]
    [ApiController]
    public class FractionsController : ControllerBase
    {
        private readonly IQuery<GetFractionsRequest, GetFractionsResponse> _getFractionsQuery;
        private readonly IQuery<GetFractionCalcDateRequest, GetFractionCalcDateResponse> _getFractionsCalcDateQuery;
        private readonly ILogger<FractionsController> _logger;


        public FractionsController(
            IQuery<GetFractionsRequest, GetFractionsResponse> getFractionsCommand,
            IQuery<GetFractionCalcDateRequest, GetFractionCalcDateResponse> getFractionCalcDateCommand,
            ILogger<FractionsController> logger)
        {
            _getFractionsQuery = getFractionsCommand;
            _getFractionsCalcDateQuery = getFractionCalcDateCommand;
            _logger = logger;
        }

        //[TypeFilter(typeof(AuthorisationFilter))]
        [HttpGet]
        [Route("epaye/{empRef1}/{empRef2}/fractions")]
        public async Task<IActionResult> GetFractions(
            string empRef1,
            string empRef2,
            DateTime fromDate,
            DateTime toDate)
        {
            _logger.LogDebug("Start GetFractions action");

            var result = await _getFractionsQuery.Get(new GetFractionsRequest($"{empRef1}/{empRef2}", fromDate, toDate));

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

            var result = await _getFractionsCalcDateQuery.Get(new GetFractionCalcDateRequest());

            _logger.LogDebug("End GetFractionCalculationDate action");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.LastCalculationDate.ToString("yyyy-MM-dd"));
        }
    }
}