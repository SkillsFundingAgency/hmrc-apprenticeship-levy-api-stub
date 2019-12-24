using System;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;


namespace SFA.DAS.HMRC.API.Stub.Infrastructure
{
    /// <summary>
    /// Azure table storeage configuration provider
    /// </summary>
    /// <seealso cref="AzureTableStorageConfigurationSource"/>
    /// <seealso cref="AzureTableStorageConfigurationExtensions"/>
    public class AzureTableStorageConfigurationProvider : ConfigurationProvider
    {
        private readonly string _connection;
        private readonly string _environment;
        private readonly string _version;
        private readonly string _appName;
        private readonly string _appStorageName;

        /// <summary>
        /// Creates a new instance of <see cref="AzureTableStorageConfigurationProvider"/>
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="appName"></param>
        /// <param name="environment"></param>
        /// <param name="version"></param>
        /// <param name="appStorageName"></param>
        public AzureTableStorageConfigurationProvider(string connection, string appName, string environment, string version, string appStorageName)
        {
            _connection = connection;
            _environment = environment;
            _version = version;
            _appName = appName;
            _appStorageName = appStorageName;
        }

        /// <summary>
        /// Loads the configuration
        /// </summary>
        public override void Load()
        {
            if (_environment.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                return;
            }

            var table = GetTable();
            var operation = GetOperation(_appStorageName, _environment, _version);
            var result = table.ExecuteAsync(operation).Result;

            var configItem = (ConfigurationItem)result.Result;

            var jsonObject = JObject.Parse(configItem.Data);

            foreach (var child in jsonObject.Children())
            {
                foreach (var jToken in child.Children().Children())
                {
                    var child1 = (JProperty)jToken;
                    Data.Add($"{child.Path}:{child1.Name}", child1.Value.ToString());
                }
            }
        }

        private CloudTable GetTable()
        {
            var storageAccount = CloudStorageAccount.Parse(_connection);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference("Configuration");
        }

        private TableOperation GetOperation(string serviceName, string environmentName, string version)
        {
            return TableOperation.Retrieve<ConfigurationItem>(environmentName, $"{serviceName}_{version}");
        }
    }
}
