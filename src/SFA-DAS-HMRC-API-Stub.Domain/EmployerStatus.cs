using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class EmployerStatus
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("empref")]
        public string EmpRef { get; set; }
        public bool Employed { get; set; }
        [BsonElement("nino")]
        public string Nino { get; set; }
        [BsonElement("fromDate")]
        public DateTime? FromDate { get; set; }
        [BsonElement("toDate")]
        public DateTime? ToDate { get; set; }
        public int? HttpStatusCode { get; set; }
    }
}
