using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Repositories;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class EmployerCheckConfig
    {
        public static IServiceCollection AddEmployerChecks(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetEmployerChecksRequest>();
            services.AddTransient<GetEmployerChecksResponse>();
            services.AddTransient<ICommand<GetEmployerChecksRequest, GetEmployerChecksResponse>, GetEmployerChecksCommand>();
            services.AddTransient<IEmployerChecksRepository, EmployerChecksCosmosRepository>(o =>
            {
                var client = o.GetRequiredService<DocumentClient>();
                var logger = o.GetRequiredService<ILogger<EmployerChecksCosmosRepository>>();
                var collectionUri = UriFactory.CreateDocumentCollectionUri(config.GetValue<string>("cosmosValues:databaseName"), "employment-status");

                return new EmployerChecksCosmosRepository(client, logger, collectionUri);
            });

            return services;
        }
    }
}
