using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Domain
{
    public class AuthRecord
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "gatewayID")]
        public string GatewayId { get; set; }
        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty(PropertyName = "refreshToken")]
        public string RefreshToken { get; set; }
        [JsonProperty(PropertyName = "privileged")]
        public bool IsPrivileged { get; set; }
        [JsonProperty(PropertyName = "clientID")]
        public string ClientId { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "refreshedAt")]
        public DateTime RefreshedAt { get; set; }
        [JsonProperty(PropertyName = "expiresIn")]
        public int ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }
    }
}
