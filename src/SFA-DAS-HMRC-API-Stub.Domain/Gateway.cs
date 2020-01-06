using MongoDB.Bson.Serialization.Attributes;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class GatewayUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }
        [BsonElement("empref")]
        public string EmpRef { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("require2SV")]
        public bool Require2SV { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("gatewayID")]
        public string GatewayId { get; set; }
    }
}
