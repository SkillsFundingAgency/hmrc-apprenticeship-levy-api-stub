using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class FractionCalculationDate
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("lastCalculationDate")]
        public DateTime LastCalculationDate { get; set; }
    }
}
