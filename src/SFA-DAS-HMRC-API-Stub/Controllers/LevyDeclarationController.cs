using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Filters;

namespace SFA_DAS_HMRC_API_Stub.Controllers
{
    [TypeFilter(typeof(AuthorisationFilter))]
    [Route("apprenticeship-levy/epaye")]
    [ApiController]
    public class LevyDeclarationController : ControllerBase
    {
        private readonly IQuery<GetLevyDeclarationRequest, GetLevyDeclarationResponse> _getLevyDeclarationQuery;
        private readonly ILogger<LevyDeclarationController> _logger;

        public LevyDeclarationController(
            IQuery<GetLevyDeclarationRequest, GetLevyDeclarationResponse> getLevyDeclarationCommand,
            ILogger<LevyDeclarationController> logger)
        {
            _getLevyDeclarationQuery = getLevyDeclarationCommand;
            _logger = logger;
        }

        [HttpGet]
        [Route("{empRef1}/{empRef2}/declarations")]
        public async Task<IActionResult> GetLevyDeclaration(
            string empRef1,
            string empRef2,
            DateTime? fromDate,
            DateTime? toDate)
        {
            _logger.LogDebug("Start GetLevyDeclaration action");

            var result = await _getLevyDeclarationQuery.Get(new GetLevyDeclarationRequest($"{empRef1}/{empRef2}", fromDate, toDate));

            _logger.LogDebug("End GetLevyDeclaration action");

            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result.Declarations);
        }
    }
}