using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class Application
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("applicationID")]
        public string ApplicationID { get; set; }
        [BsonElement("clientID")]
        public string ClientID { get; set; }
        [BsonElement("clientSecret")]
        public string ClientSecret { get; set; }
        [BsonElement("serverToken")]
        public string ServerToken { get; set; }
        [BsonElement("privilegedAccess")]
        public bool PrivilegedAccess { get; set; }
    }
}
