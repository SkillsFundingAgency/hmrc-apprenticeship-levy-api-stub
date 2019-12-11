using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class EmployerStatus
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string EmpRef { get; set; }
        public bool Employed { get; set; }
        public string Nino { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? HttpStatusCode { get; set; }
    }
}
