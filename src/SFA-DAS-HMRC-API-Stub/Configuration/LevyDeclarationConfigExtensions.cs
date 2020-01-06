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
    public static class LevyDeclarationConfig
    {
        public static IServiceCollection AddLevyDeclaration(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetLevyDeclarationRequest>();
            services.AddTransient<GetLevyDeclarationResponse>();
            services.AddTransient<IQuery<GetLevyDeclarationRequest, GetLevyDeclarationResponse>, GetLevyDeclarationQuery>();
            services.AddTransient<ILevyDeclarationRepository, LevyDeclarationRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<LevyDeclarationRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new LevyDeclarationRepository(database, logger);
            });

            return services;
        }
    }
}
