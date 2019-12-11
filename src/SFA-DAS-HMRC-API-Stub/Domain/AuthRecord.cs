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
        public string GatewayId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        [JsonProperty(PropertyName = "Privileged")]
        public bool IsPrivileged { get; set; }
        public string ClientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime RefreshedAt { get; set; }
        public int ExpiresIn { get; set; }
        public string Scope { get; set; }
    }
}
