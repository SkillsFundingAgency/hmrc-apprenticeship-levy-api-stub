using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using SFA.DAS.HMRC.API.Stub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Filters
{
    public class AuthorisationFilter : Attribute, IActionFilter
    {
        private readonly IAuthenticate _authService;

        public AuthorisationFilter(IAuthenticate authService)
        {
            _authService = authService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Not required
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Claims.First(c => c.Type == "privileged").Value.ToLower() == "true")
            {
                return;
            }

            var gatewayId = context.HttpContext.User.Claims.First(c => c.Type == "gatewayid").Value;

            var parts = context.ActionArguments
                .Where(x => x.Key == "empRef1" || x.Key == "empRef2")
                .ToArray()
            ;

            if (parts.Count() < 2 || string.IsNullOrWhiteSpace(gatewayId))
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            var empRef = $"{parts[0].Value}/{parts[1].Value}";

            var isAuthenticated = _authService.IsAuthorized(gatewayId, empRef).Result;
            if (!isAuthenticated)
            {
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}