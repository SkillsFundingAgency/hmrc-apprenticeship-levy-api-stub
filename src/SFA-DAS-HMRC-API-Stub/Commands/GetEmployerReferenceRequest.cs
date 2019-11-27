using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetEmployerReferenceRequest
    {
        public string EmpRef { get; set; }

        public GetEmployerReferenceRequest(string empRef)
        {
            EmpRef = empRef; 
        }
    }
}
