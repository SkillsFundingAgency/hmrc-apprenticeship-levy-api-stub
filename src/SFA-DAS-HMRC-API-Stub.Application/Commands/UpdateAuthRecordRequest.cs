using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.HMRC.API.Stub.Application.Commands
{
    public class UpdateAuthRecordRequest
    {
        public string RefreshToken { get; set; }
        public AuthRecord AuthRecord { get; set; }
    }
}
