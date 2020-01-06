using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo;
using SFA.DAS.HMRC.API.Stub.Repositories;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class EmployerCheckConfig
    {
        public static IServiceCollection AddEmployerChecks(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetEmployerChecksRequest>();
            services.AddTransient<GetEmployerChecksResponse>();
            services.AddTransient<IQuery<GetEmployerChecksRequest, GetEmployerChecksResponse>, GetEmployerChecksQuery>();

            services.AddTransient<IEmployerChecksRepository, EmployerChecksRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<EmployerChecksRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new EmployerChecksRepository(database, logger);
            });

            return services;
        }
    }
}
