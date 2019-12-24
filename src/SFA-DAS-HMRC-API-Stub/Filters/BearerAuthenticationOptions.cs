using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Filters
{
    /// <summary>
    /// Contains the bearer authentication options
    /// </summary>
    public class BearerAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// THe default scheme for bearer authentication
        /// </summary>
        public static string DefaultScheme { get; set; } = "Bearer";
    }
}
