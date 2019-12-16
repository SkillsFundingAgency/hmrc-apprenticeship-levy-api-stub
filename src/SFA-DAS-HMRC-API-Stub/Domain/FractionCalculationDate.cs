using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class FractionCalculationDate
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "lastcalculationdate")]
        public List<Calculation> LastCalculationDate { get; set; }
    }

    public class Calculation
    {
        [JsonProperty(PropertyName = "calculationdate")]
        public DateTime CalculationDate { get; set; }
    }



}
