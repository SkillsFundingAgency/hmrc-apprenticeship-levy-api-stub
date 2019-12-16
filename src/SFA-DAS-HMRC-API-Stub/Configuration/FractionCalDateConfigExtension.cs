using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class FractionCalDateConfig
    {
        public static IServiceCollection FractionCalDate(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetFractionCalDateRequest>();
            services.AddTransient<GetFractionCalDateResponse>();
            services.AddTransient<ICommand<GetFractionCalDateRequest, GetFractionCalDateResponse>, GetFractionCalDateCommand>();
            
            services.AddTransient<IFractionCalDateRepository, FractionCalDateCosmosRepository>(o =>
            {
                var client = o.GetRequiredService<DocumentClient>();
                var logger = o.GetRequiredService<ILogger<FractionCalDateCosmosRepository>>();
                var collectionUri = UriFactory.CreateDocumentCollectionUri(config.GetValue<string>("cosmosValues:databaseName"), Constants.FRACTIONCALDATE);

                return new FractionCalDateCosmosRepository(client, logger, collectionUri);
            });

            return services;
        }
    }
}
