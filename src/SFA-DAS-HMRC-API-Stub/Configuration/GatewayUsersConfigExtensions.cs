using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class GatewayUsersConfig
    {
        public static IServiceCollection AddGatewayUsers(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IGatewayRepository, GatewayRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<GatewayRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new GatewayRepository(database, logger);
            });

            return services;
        }
    }
}
