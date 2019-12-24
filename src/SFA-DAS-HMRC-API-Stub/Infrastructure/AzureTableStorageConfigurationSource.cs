using Microsoft.Extensions.Configuration;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure
{
    /// <summary>
    /// Azure table storage configuration source
    /// </summary>
    /// <seealso cref="AzureTableStorageConfigurationProvider"/>
    /// <seealso cref="AzureTableStorageConfigurationExtensions"/>
    public class AzureTableStorageConfigurationSource : IConfigurationSource
    {
        private readonly string _connection;
        private readonly string _environment;
        private readonly string _version;
        private readonly string _appStorageName;
        private readonly string _appName;

        /// <summary>
        /// Creates a new instance of <see cref="AzureTableStorageConfigurationSource"/>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="appName"></param>
        /// <param name="environment"></param>
        /// <param name="version"></param>
        /// <param name="appStorageName"></param>
        public AzureTableStorageConfigurationSource(string connection, string appName, string environment, string version, string appStorageName)
        {
            _appName = appName;
            _connection = connection;
            _environment = environment;
            _version = version;
            _appStorageName = appStorageName;
        }

        /// <summary>
        /// Builds the configuration provider
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AzureTableStorageConfigurationProvider(_connection, _appName, _environment, _version, _appStorageName);
        }
    }
}
