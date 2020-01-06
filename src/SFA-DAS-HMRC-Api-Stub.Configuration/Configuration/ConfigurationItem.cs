using Microsoft.Azure.Cosmos.Table;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure
{
    /// <summary>
    /// Configuration item
    /// </summary>
    public class ConfigurationItem : TableEntity
    {
        /// <summary>
        /// Configuration data
        /// </summary>
        public string Data { get; set; }
    }
}
