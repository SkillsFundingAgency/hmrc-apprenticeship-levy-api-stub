using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Filters
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<BearerAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<BearerAuthenticationOptions, BearerAuthenticationHandler>("Bearer", configureOptions);
        }
    }
}
