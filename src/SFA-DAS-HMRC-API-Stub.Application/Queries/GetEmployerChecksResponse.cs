using System;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetEmployerChecksResponse
    {
        public string Empref { get; set; }
        public string Nino { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool Employed { get; set; }
    }
}