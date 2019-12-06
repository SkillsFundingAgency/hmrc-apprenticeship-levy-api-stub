using Microsoft.Azure.Cosmos.Table;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure
{
    public class ConfigurationItem : TableEntity
    {
        public string Data { get; set; }
    }
}
