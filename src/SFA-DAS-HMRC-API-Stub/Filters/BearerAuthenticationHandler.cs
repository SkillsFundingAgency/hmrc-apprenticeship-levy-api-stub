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
    /// <summary>
    /// Filter handles authentication on incoming requests
    /// </summary>
    /// <seealso cref="BearerAuthenticationOptions"/>
    /// <seealso cref="AuthorisationFilter"/>
    /// <seealso cref="AuthenticationBuilderExtensions"/>
    public class BearerAuthenticationHandler : AuthenticationHandler<BearerAuthenticationOptions>
    {
        private readonly IAuthenticate _authService;

        /// <summary>
        /// Creates a new instance of <see cref="BearerAuthenticationHandler"/>
        /// </summary>
        /// <param name="options">Configuration options</param>
        /// <param name="logger">Logger</param>
        /// <param name="encoder">Url encoder</param>
        /// <param name="clock">System clock</param>
        /// <param name="authService">Authentication service</param>
        public BearerAuthenticationHandler(
            IOptionsMonitor<BearerAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IAuthenticate authService) : base(options, logger, encoder, clock)
        {
            _authService = authService;
        }

        /// <summary>
        /// Handles authentication on incoming requests by checking the authorization header for a valid bearer token.
        /// Where no token exists or it is invalid an Unauthorized response is returned
        /// Where the token is valid a <see cref="AuthenticationTicket"/> is returned containing a <see cref="ClaimsPrincipal"/> with the appropriate <see cref="ClaimsIdentity"/>
        /// </summary>
        /// <returns></returns>
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
