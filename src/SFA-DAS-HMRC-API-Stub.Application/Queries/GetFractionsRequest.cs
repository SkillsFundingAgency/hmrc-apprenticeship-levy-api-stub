using System;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
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
