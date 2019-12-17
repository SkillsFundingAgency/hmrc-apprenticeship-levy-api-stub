using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class Fractions
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "empref")]
        public string EmpRef { get; set; }
        public List<FractionCalculations> FractionCalculation { get; set; }
    }

    public class FractionData
    {
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }
        [JsonProperty(PropertyName = "value")]
        public decimal Value { get; set; }
    }

    public class FractionCalculations
    {
        [JsonProperty(PropertyName = "calculatedat")]
        public DateTime CalculatedAt { get; set; }
        [JsonProperty(PropertyName = "fraction")]
        public Fractions Fraction { get; set; }
    }
}
