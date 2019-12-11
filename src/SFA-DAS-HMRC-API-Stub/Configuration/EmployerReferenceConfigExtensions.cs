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
    public static class EmployerReferenceConfig
    {
        public static IServiceCollection AddEmployerReference(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetEmployerReferenceRequest>();
            services.AddTransient<GetEmployerrReferenceResponse>();
            services.AddTransient<ICommand<GetEmployerReferenceRequest, GetEmployerrReferenceResponse>, GetEmployerReferenceCommand>();

            services.AddTransient<IEmployerReferenceRepository, EmployerReferenceCosmosRepository>(o =>
            {
                var client = o.GetRequiredService<DocumentClient>();
                var logger = o.GetRequiredService<ILogger<EmployerReferenceCosmosRepository>>();
                var collectionUri = UriFactory.CreateDocumentCollectionUri(config.GetValue<string>("cosmosValues:databaseName"), Constants.EMPLOYERREFERENCE);

                return new EmployerReferenceCosmosRepository(client, logger, collectionUri);
            });

            return services;
        }
    }
}
