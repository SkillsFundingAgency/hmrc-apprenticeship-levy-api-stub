using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class Fractions
    {
        [BsonElement("region")]
        public string Region { get; set; }
        [BsonElement("value")]
        public string Value { get; set; }
    }

    public class FractionCalculation
    {
        [BsonElement("calculatedAt")]
        public DateTime CalculatedAt { get; set; }
        [BsonElement("fractions")]
        public List<Fractions> Fractions { get; set; }
    }

    public class RootObject
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("empref")]
        public string EmpRef { get; set; }
        [BsonElement("fractionCalculations")]
        public List<FractionCalculation> FractionCalculations { get; set; }
    }
}
