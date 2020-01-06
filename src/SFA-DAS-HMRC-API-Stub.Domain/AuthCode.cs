using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class AuthCode
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("authorizationCode")]
        public string AuthorizationCode { get; set; }
        [BsonElement("gatewayId")]
        public string GatewayId { get; set; }
        [BsonElement("redirectUri")]
        public string RedirectUri { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("scope")]
        public string Scope { get; set; }
        [BsonElement("clientId")]
        public string ClientId { get; set; }
        [BsonElement("ExpiresIn")]
        public int expiresIn { get; set; }
    }
}
