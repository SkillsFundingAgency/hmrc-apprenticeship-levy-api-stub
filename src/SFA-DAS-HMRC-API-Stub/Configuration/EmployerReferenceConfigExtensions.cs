using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class EmployerReferenceConfig
    {
        public static IServiceCollection AddEmployerReference(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetEmployerReferenceRequest>();
            services.AddTransient<GetEmployerrReferenceResponse>();
            services.AddTransient<IQuery<GetEmployerReferenceRequest, GetEmployerrReferenceResponse>, GetEmployerReferenceQuery>();

            services.AddTransient<IEmployerReferenceRepository, EmployerReferenceRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<EmployerReferenceRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new EmployerReferenceRepository(database, logger);
            });

            return services;
        }
    }
}
