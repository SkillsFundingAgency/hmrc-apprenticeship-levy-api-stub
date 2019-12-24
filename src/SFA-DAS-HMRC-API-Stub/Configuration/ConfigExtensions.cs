using Microsoft.Extensions.Configuration;
using SFA.DAS.HMRC.API.Stub.Infrastructure;
using System.IO;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    /// <summary>
    /// Configration extensions
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Adds configuration to the <see cref="IConfigurationBuilder"/> from files and table storage
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IConfiguration AddConfig(this IConfigurationBuilder builder, IConfiguration config)
        {
            return builder
                .AddConfiguration(config)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .AddJsonFile("appSettings.Development.json", true)
                .AddEnvironmentVariables()
                .AddAzureTableStorageConfiguration(
                    config["ConfigurationStorageConnectionString"],
                    config["AppName"],
                    config["EnvironmentName"],
                    "1.0", "SFA.DAS.HmrcApprenticeshipLevyApiStub")
                .Build()
            ;
        }
    }
}
