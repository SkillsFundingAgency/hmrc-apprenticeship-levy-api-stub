using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class Declaration
    {
        public string EmpRef { get; set; }
        public int Id { get; set; }
        public DateTime SubmissionTime { get; set; }
        public string Data { get; set; }
    }
}
