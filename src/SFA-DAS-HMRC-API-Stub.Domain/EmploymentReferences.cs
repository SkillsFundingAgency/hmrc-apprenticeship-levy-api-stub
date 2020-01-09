using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class EmployerReference
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("_links")]
        public Links Links { get; set; }
        [BsonElement("empref")]
        public string EmpRef { get; set; }
        [BsonElement("employer")]
        public Employer Employer { get; set; }
    }

    public class Self
    {
        [BsonElement("href")]
        public string Href { get; set; }
    }

    public class Declarations
    {
        [BsonElement("href")]
        public string Href { get; set; }
    }

    public class Fraction
    {
        [BsonElement("href")]
        public string Href { get; set; }
    }

    public class EmploymentCheck
    {
        [BsonElement("href")]
        public string Href { get; set; }
    }

    public class Links
    {
        [BsonElement("self")]
        public Self Self { get; set; }
        [BsonElement("declarations")]
        public Declarations Declarations { get; set; }
        [BsonElement("fractions")]
        public Fraction Fraction { get; set; }
        [BsonElement("employment-check")]
        public EmploymentCheck EmploymentCheck { get; set; }
}

    public class Name
    {
        [BsonElement("nameLine1")]
        public string NameLine1 { get; set; }
    }

    public class Employer
    {
        [BsonElement("name")]
        public Name Name { get; set; }
    }

}