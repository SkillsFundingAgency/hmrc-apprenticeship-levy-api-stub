using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    [BsonIgnoreExtraElements]
    public class AuthRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId ObjectId { get; set; }
        [BsonElement("scope")]
        public string Scope { get; set; }
        [BsonElement("clientId")]
        public string ClientId { get; set; }
        [BsonElement("redirectUri")]
        public string RedirectUri { get; set; }
        [BsonElement("id")]
        public long Id { get; set; }
        [BsonElement("creationDate")]
        public DateTime CreationDate { get; set; }
        public string State { get; set; }
    }
}