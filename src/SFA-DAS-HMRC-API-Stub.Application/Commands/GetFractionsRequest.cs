using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetFractionsRequest
    {
        public string EmpRef { get; }
        public DateTime FromDate { get; }
        public DateTime ToDate { get; }

        public GetFractionsRequest(string empRef, DateTime fromDate, DateTime toDate)
        {
            this.EmpRef = empRef;
            this.FromDate = fromDate;
            this.ToDate = toDate;
        }
    }
}
