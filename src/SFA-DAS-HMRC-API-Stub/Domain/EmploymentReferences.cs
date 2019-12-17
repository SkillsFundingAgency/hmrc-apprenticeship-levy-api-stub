using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class EmployerReference
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "_links")]
        public Links Links { get; set; }
        [JsonProperty(PropertyName = "empref")]
        public string EmpRef { get; set; }
        [JsonProperty(PropertyName = "employer")]
        public Employer Employer { get; set; }
    }

    public class Self
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
    }

    public class Declarations
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
    }
    
    public class Fraction
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
    }

    public class EmploymentCheck
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
    }

    public class Links
    {
        [JsonProperty(PropertyName = "self")]
        public Self Self { get; set; }
        [JsonProperty(PropertyName = "declarations")]
        public Declarations Declarations { get; set; }
        [JsonProperty(PropertyName = "fractions")]
        public Fractions Fractions { get; set; }
        [JsonProperty(PropertyName = "employment-check")]
        public EmploymentCheck EmploymentCheck { get; set; }
}

    public class Name
    {
        [JsonProperty(PropertyName = "nameLine1")]
        public string NameLine1 { get; set; }
    }

    public class Employer
    {
        [JsonProperty(PropertyName = "name")]
        public Name Name { get; set; }
    }

}