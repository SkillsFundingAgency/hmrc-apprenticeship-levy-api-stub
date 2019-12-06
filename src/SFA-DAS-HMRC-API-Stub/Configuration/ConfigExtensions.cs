using Microsoft.Extensions.Configuration;
using SFA.DAS.HMRC.API.Stub.Infrastructure;
using System.IO;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class Config
    {
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
