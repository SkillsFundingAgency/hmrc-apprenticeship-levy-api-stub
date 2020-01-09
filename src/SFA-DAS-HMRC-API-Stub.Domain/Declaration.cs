using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class LevyDeclaration
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonIgnore]
        public string Id { get; set; }
        [BsonElement("empref")]
        public string EmpRef { get; set; }
        [BsonElement("declarations")]
        public List<Declaration> Declarations { get; set; }
    }

    public class PayrollPeriod
    {
        [BsonElement("year")]
        public string Year { get; set; }
        [BsonElement("month")]
        public int Month { get; set; }
    }

    [BsonNoId] // https://stackoverflow.com/questions/55406944/element-id-does-not-match-any-field-or-property-of-error-with-nested-classes (MongoDB driver registers a convention of id mapping it to a regular BSONID. This breaks when you have a non BSONID id field in the document
    public class Declaration
    {
        [BsonElement("id")]
        public long Id { get; set; }
        [BsonElement("submissionTime")]
        public DateTime SubmissionTime { get; set; }
        [BsonElement("payrollPeriod")]
        public PayrollPeriod PayrollPeriod { get; set; }
        [BsonElement("levyDueYTD")]
        public decimal LevyDueYTD { get; set; }
        [BsonElement("levyAllowanceForFullYear")]
        public decimal LevyAllowanceForFullYear { get; set; }
    }
}
