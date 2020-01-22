using System;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetLevyDeclarationRequest
    {
        public string EmpRef { get; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public GetLevyDeclarationRequest(string empRef, DateTime? fromDate, DateTime? toDate)
        {
            this.EmpRef = empRef;
            this.FromDate = fromDate;
            this.ToDate = toDate;
        }
    }
}
