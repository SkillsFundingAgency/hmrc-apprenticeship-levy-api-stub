using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class LevyDeclarationConfig
    {
        public static IServiceCollection AddLevyDeclaration(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetLevyDeclarationRequest>();
            services.AddTransient<GetLevyDeclarationResponse>();
            services.AddTransient<ICommand<GetLevyDeclarationRequest, GetLevyDeclarationResponse>, GetLevyDeclarationCommand>();
            services.AddTransient<ILevyDeclarationRepository, LevyDeclarationCosmosRepository>(o =>
            {
                var client = o.GetRequiredService<DocumentClient>();
                var logger = o.GetRequiredService<ILogger<LevyDeclarationCosmosRepository>>();
                var collectionUri = UriFactory.CreateDocumentCollectionUri(config.GetValue<string>("cosmosValues:databaseName"), Constants.LEVYDECLARATION);

                return new LevyDeclarationCosmosRepository(client, logger, collectionUri);
            });

            return services;
        }
    }
}
