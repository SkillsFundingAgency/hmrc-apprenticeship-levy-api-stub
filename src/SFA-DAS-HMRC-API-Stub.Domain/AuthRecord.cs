using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class AuthRecord
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId ObjectId { get; set; }
        [BsonElement("gatewayID")]
        public string GatewayId { get; set; }
        [BsonElement("accessToken")]
        public string AccessToken { get; set; }
        [BsonElement("refreshToken")]
        public string RefreshToken { get; set; }
        [BsonElement("privileged")]
        public bool IsPrivileged { get; set; }
        [BsonElement("clientID")]
        public string ClientId { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("refreshedAt")]
        public DateTime RefreshedAt { get; set; }
        [BsonElement("expiresIn")]
        public int ExpiresIn { get; set; }
        [BsonElement("scope")]
        public string Scope { get; set; }
    }
}
