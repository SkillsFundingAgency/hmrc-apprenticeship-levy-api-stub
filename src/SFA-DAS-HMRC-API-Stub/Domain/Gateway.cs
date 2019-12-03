using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class GatewayUser
    {
        public int Id { get; set; }
        public string EmpRef { get; set; }
        public string Name { get; set; }
        public bool Require2SV { get; set; }
        public string Password { get; set; }
        public string GatewayId { get; set; }
    }
}
