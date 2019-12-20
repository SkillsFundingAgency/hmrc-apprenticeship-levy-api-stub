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
        [JsonProperty(PropertyName = "empref")]
        public string EmpRef { get; set; }
        public bool Employed { get; set; }
        [JsonProperty(PropertyName = "nino")]
        public string Nino { get; set; }
        [JsonProperty(PropertyName = "fromDate")]
        public DateTime? FromDate { get; set; }
        [JsonProperty(PropertyName = "toDate")]
        public DateTime? ToDate { get; set; }
        public int? HttpStatusCode { get; set; }
    }
}
