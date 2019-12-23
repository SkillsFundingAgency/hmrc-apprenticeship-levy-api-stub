using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class LevyDeclaration
    {
        [JsonProperty(PropertyName = "empref")]
        public string EmpRef { get; set; }
        [JsonProperty(PropertyName = "declarations")]
        public List<Declaration> Declarations { get; set; }
    }

    public class PayrollPeriod
    {
        [JsonProperty(PropertyName = "year")]
        public string Year { get; set; }
        [JsonProperty(PropertyName = "month")]
        public int Month { get; set; }
    }

    public class Declaration
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "submissionTime")]
        public DateTime SubmissionTime { get; set; }
        [JsonProperty(PropertyName = "payrollPeriod")]
        public PayrollPeriod PayrollPeriod { get; set; }
        [JsonProperty(PropertyName = "levyDueYTD")]
        public int LevyDueYTD { get; set; }
        [JsonProperty(PropertyName = "levyAllowanceForFullYear")]
        public int LevyAllowanceForFullYear { get; set; }
    }
}
