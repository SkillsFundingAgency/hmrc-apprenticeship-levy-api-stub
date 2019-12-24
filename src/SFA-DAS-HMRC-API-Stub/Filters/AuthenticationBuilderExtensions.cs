using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Filters
{
    /// <summary>
    /// Provides authentication builder extensions
    /// </summary>
    public static class AuthenticationBuilderExtensions
    {
        /// <summary>
        /// Adds a custom authetication scheme
        /// </summary>
        /// <param name="builder">Authentication builder</param>
        /// <param name="configureOptions">Configuration options</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<BearerAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<BearerAuthenticationOptions, BearerAuthenticationHandler>("Bearer", configureOptions);
        }
    }
}
