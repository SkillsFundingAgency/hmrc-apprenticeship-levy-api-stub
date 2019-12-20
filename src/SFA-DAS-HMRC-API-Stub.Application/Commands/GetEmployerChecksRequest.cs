using System;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetEmployerChecksRequest
    {
        public string EmpRef { get; }
        public string Nino { get; }
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }

        public GetEmployerChecksRequest(string empRef, string nino, DateTime? fromDate, DateTime? toDate)
        {
            this.EmpRef = empRef;
            this.Nino = nino;
            this.FromDate = fromDate;
            this.ToDate = toDate;
        }
    }
}