using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using SFA.DAS.HMRC.API.Stub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Filters
{
    public class BearerAuthenticationHandler : AuthenticationHandler<BearerAuthenticationOptions>
    {
        private readonly IAuthenticate _authService;

        public BearerAuthenticationHandler(IOptionsMonitor<BearerAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthenticate authService) : base(options, logger, encoder, clock)
        {
            _authService = authService;
        }

        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
                {
                    return AuthenticateResult.Fail("No Authorization header found");
                }

                if (!Request.Headers[HeaderNames.Authorization].Any(h => h.Contains(BearerAuthenticationOptions.DefaultScheme)))
                {
                    return AuthenticateResult.Fail("Authorization header should be a Bearer token");
                }

                var token = Request.Headers[HeaderNames.Authorization]
                    .First()
                    .Split(' ')
                    [1]
                ;

                var response = await _authService.IsAuthenticated(token);
                if (!response.IsAuthenticated)
                {
                    return AuthenticateResult.Fail("Bearer token does not grant access to the requested resource");
                }

                var principal = new ClaimsPrincipal
                (
                    new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("gatewayid", response.GatewayId ?? string.Empty),
                        new Claim("privileged", response.IsPrivileged.ToString())
                    }, BearerAuthenticationOptions.DefaultScheme)
                );

                return AuthenticateResult.Success(
                    new AuthenticationTicket(principal, BearerAuthenticationOptions.DefaultScheme)
                );
            }
            catch(Exception ex)
            {
                return AuthenticateResult.Fail("Failed to authenticate request");
            }
        }
    }
}
