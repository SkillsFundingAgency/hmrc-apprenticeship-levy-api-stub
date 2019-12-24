using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SFA.DAS.HMRC.API.Stub.Services;
using System;
using System.Linq;

namespace SFA.DAS.HMRC.API.Stub.Filters
{
    /// <summary>
    /// Authorisation Filter
    /// Should only be used where there is empRef1 and empRef2 parameters are passed to the action otherwise a <see cref="BadRequestObjectResult"/> is returned in the result
    /// </summary>
    public class AuthorisationFilter : Attribute, IActionFilter
    {
        private readonly IAuthenticate _authService;

        /// <summary>
        /// Creates a new instance of <see cref="AuthorisationFilter"/>
        /// </summary>
        /// <param name="authService"></param>
        public AuthorisationFilter(IAuthenticate authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Fires after the action has been executed
        /// Performs no action in this instance
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Not required
        }

        /// <summary>
        /// Fires before the action has been executed
        /// Checks that there is a valid gatewayid claim for the user and validates that the gatewayid is valid for the empRef1/empRef2 action arguments
        /// If the user is privileged then the empRef1/empRef2 check is skipped as the user is deemed to have access to all employer data
        /// </summary>
        /// <param name="context"></param>
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