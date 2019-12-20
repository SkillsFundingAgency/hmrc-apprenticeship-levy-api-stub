using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class Fractions
    {
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

    public class FractionCalculation
    {
        [JsonProperty(PropertyName = "calculatedat")]
        public DateTime CalculatedAt { get; set; }
        [JsonProperty(PropertyName = "fractions")]
        public List<Fractions> Fractions { get; set; }
    }

    public class RootObject
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "empref")]
        public string EmpRef { get; set; }
        [JsonProperty(PropertyName = "fractionCalculations")]
        public List<FractionCalculation> FractionCalculations { get; set; }
    }
}
