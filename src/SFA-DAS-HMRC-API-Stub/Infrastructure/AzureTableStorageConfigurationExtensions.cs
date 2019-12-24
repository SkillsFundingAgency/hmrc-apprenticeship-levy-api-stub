using Microsoft.Extensions.Configuration;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure
{
    /// <summary>
    /// Azure table storage extensions
    /// </summary>
    /// <seealso cref="AzureTableStorageConfigurationSource"/>
    /// <seealso cref="AzureTableStorageConfigurationProvider"/>
    public static class AzureTableStorageConfigurationExtensions
    {
        /// <summary>
        /// Adds Azure table storage configuration
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="connection"></param>
        /// <param name="appName"></param>
        /// <param name="environment"></param>
        /// <param name="version"></param>
        /// <param name="appStorageName"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddAzureTableStorageConfiguration(this IConfigurationBuilder builder, string connection, string appName, string environment, string version, string appStorageName)
        {
            return builder.Add(new AzureTableStorageConfigurationSource(connection, appName, environment, version, appStorageName));
        }
    }
}
