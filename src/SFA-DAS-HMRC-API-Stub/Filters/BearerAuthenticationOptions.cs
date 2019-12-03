using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Filters
{
    public class BearerAuthenticationOptions : AuthenticationSchemeOptions
    {
        public static string DefaultScheme { get; set; } = "Bearer";
    }
}
