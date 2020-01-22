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
    public static class FractionsConfigExtensions
    {
        public static IServiceCollection AddFractions(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetFractionsRequest>();
            services.AddTransient<GetFractionsResponse>();
            services.AddTransient<IQuery<GetFractionsRequest, GetFractionsResponse>, GetFractionsQuery>();

            services.AddTransient<IFractionsRepository, FractionsRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<FractionsRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new FractionsRepository(database, logger);
            });

            return services;
        }

        public static IServiceCollection AddFractionCalcDate(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetFractionCalcDateRequest>();
            services.AddTransient<GetFractionCalcDateResponse>();
            services.AddTransient<IQuery<GetFractionCalcDateRequest, GetFractionCalcDateResponse>, GetFractionCalcDateQuery>();

            services.AddTransient<IFractionsCalcDateRepository, FractionsRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<FractionsRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new FractionsRepository(database, logger);
            });

            return services;
        }
    }
}
