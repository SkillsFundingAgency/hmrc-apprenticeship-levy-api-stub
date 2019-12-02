using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;

namespace SFA_DAS_HMRC_API_Stub.Controllers
{
    [ApiController]
    public class LevyDeclarationController : ControllerBase
    {
        private readonly ICommand<GetLevyDeclarationRequest, GetLevyDeclarationResponse> _getLevyDeclarationCommand;
        private readonly ILogger<LevyDeclarationController> _logger;

        public LevyDeclarationController(
            ICommand<GetLevyDeclarationRequest, GetLevyDeclarationResponse> getLevyDeclarationCommand,
            ILogger<LevyDeclarationController> logger)
        {
            _getLevyDeclarationCommand = getLevyDeclarationCommand;
            _logger = logger;

        }

        [HttpGet]
        [Route("apprenticeship-levy/epaye/{empRef1}/{empRef2}/employed/{nino}")]

        public async Task<IActionResult> GetLevyDeclaration()
        {
            _logger.LogDebug("Start GetLevyDeclaration action");

            var result = await _getLevyDeclarationCommand.Get(new GetLevyDeclarationRequest());

            _logger.LogDebug("End GetLevyDeclaration action");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}