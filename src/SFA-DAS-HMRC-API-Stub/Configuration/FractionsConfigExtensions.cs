using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Data.Repositories.Cosmos;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class FractionsConfigExtensions
    {
        public static IServiceCollection AddFractions(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetFractionsRequest>();
            services.AddTransient<GetFractionsResponse>();
            services.AddTransient<ICommand<GetFractionsRequest, GetFractionsResponse>, GetFractionsCommand>();
            services.AddTransient<IFractionsRepository, FractionsCosmosRepository>(o =>
            {
                var client = o.GetRequiredService<DocumentClient>();
                var logger = o.GetRequiredService<ILogger<FractionsCosmosRepository>>();
                var collectionUri = UriFactory.CreateDocumentCollectionUri(config.GetValue<string>("cosmosValues:databaseName"), Constants.FRACTIONS);

                return new FractionsCosmosRepository(client, logger, collectionUri);
            });

            return services;
        }

        public static IServiceCollection AddFractionCalcDate(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetFractionCalcDateRequest>();
            services.AddTransient<GetFractionCalcDateResponse>();
            services.AddTransient<ICommand<GetFractionCalcDateRequest, GetFractionCalcDateResponse>, GetFractionCalcDateCommand>();

            services.AddTransient<IFractionsCalcDateRepository, FractionsCosmosRepository>(o =>
            {
                var client = o.GetRequiredService<DocumentClient>();
                var logger = o.GetRequiredService<ILogger<FractionsCosmosRepository>>();
                var collectionUri = UriFactory.CreateDocumentCollectionUri(config.GetValue<string>("cosmosValues:databaseName"), Constants.FRACTIONCALCDATE);

                return new FractionsCosmosRepository(client, logger, collectionUri);
            });

            return services;
        }
    }
}
