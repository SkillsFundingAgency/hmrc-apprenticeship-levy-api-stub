﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Filters;

namespace SFA_DAS_HMRC_API_Stub.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = "Bearer")]
    [TypeFilter(typeof(AuthorisationFilter))]
    [Route("apprenticeship-levy/epaye")]
    [ApiController]
    public class EmpRefController : ControllerBase
    {
        private readonly IQuery<GetEmployerReferenceRequest, GetEmployerrReferenceResponse> _getEmployerReference;
        private readonly ILogger<EmpRefController> _logger;

        public EmpRefController(
            IQuery<GetEmployerReferenceRequest, GetEmployerrReferenceResponse> getEmployerReference,
            ILogger<EmpRefController> logger)
        {
            _getEmployerReference = getEmployerReference;
            _logger = logger;
        }

        [HttpGet]
        [Route("{empRef1}/{empRef2}")]
        public async Task<IActionResult> GetEmploymentRef(
           string empRef1,
           string empRef2)
        {
            _logger.LogDebug("Start GetEmploymentRef action");

            var result = await _getEmployerReference.Get(new GetEmployerReferenceRequest($"{empRef1}/{empRef2}"));

            _logger.LogDebug("End GetEmploymentRef action");

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.EmployerReference);
        }
    }
}