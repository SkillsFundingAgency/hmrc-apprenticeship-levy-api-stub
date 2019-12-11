using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class GatewayUsersConfig
    {
        public static IServiceCollection AddGatewayUsers(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IGatewayRepository, GatewayCosmosRepository>(o =>
            {
                var client = o.GetRequiredService<DocumentClient>();
                var logger = o.GetRequiredService<ILogger<GatewayCosmosRepository>>();
                var collectionUri = UriFactory.CreateDocumentCollectionUri(config.GetValue<string>("cosmosValues:databaseName"), Constants.GATEWAYUSERS);

                return new GatewayCosmosRepository(client, logger, collectionUri);
            });

            return services;
        }
    }
}
