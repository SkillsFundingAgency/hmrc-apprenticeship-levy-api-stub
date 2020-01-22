using Microsoft.Extensions.Configuration;
using SFA.DAS.HMRC.API.Stub.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SFA_DAS_TaxService_Stub.Configuration
{
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
