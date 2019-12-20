using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Services
{
    public class AuthResponse
    {
        public bool IsAuthenticated { get; set; }
        public string GatewayId { get; set; }
        public bool IsPrivileged { get; set; }

    }
}
