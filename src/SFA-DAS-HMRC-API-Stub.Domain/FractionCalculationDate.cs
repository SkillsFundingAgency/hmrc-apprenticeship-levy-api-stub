using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class FractionCalculationDate
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "lastcalculationdate")]
        public DateTime LastCalculationDate { get; set; }
    }
}
