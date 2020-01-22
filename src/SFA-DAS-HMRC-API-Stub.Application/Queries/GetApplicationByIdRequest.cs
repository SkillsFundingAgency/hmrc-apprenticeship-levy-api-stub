using System;
using System.Collections.Generic;
using System.Text;
using Domain = SFA.DAS.HMRC.API.Stub.Domain;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetApplicationByIdRequest
    {
        public string ClientId { get; set; }
    }
}
