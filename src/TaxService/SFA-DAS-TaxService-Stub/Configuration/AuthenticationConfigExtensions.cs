using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Application.Commands;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo;
using SFA.DAS.HMRC.API.Stub.Services;

namespace SFA_DAS_TaxService_Stub.Configuration
{
    public static class AuthenticationConfigExtensions
    {
        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IQuery<GetAuthRequestByIdRequest, GetAuthRequestByIdResponse>, GetAuthRequestByIdQuery>();
            services.AddTransient<IQuery<GetScopeByNameRequest, GetScopeByNameResponse>, GetScopeByNameQuery>();
            services.AddTransient<ICommand<DeleteAuthRequestRequest, DeleteAuthRequestResponse>, DeleteAuthRequestByIdCommand>();
            services.AddTransient<ICommand<InsertAuthCodeRequest, InsertAuthCodeResponse>, InsertAuthCodeCommand>();

            services.AddTransient<IAuthRequestRepository, AuthRequestRepository>(o =>
                {
                    var client = o.GetRequiredService<MongoClient>();
                    var logger = o.GetRequiredService<ILogger<AuthRequestRepository>>();
                    var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                    return new AuthRequestRepository(database, logger);
                });

            services.AddTransient<IAuthRecordRepository, AuthRecordRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<AuthRecordRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new AuthRecordRepository(database, logger);
            });

            services.AddTransient<IAuthCodeRepository, AuthCodeRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<AuthCodeRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new AuthCodeRepository(database, logger);
            });

            services.AddTransient<IScopeRepository, ScopeRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<ScopeRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new ScopeRepository(database, logger);
            });            

            services.AddTransient<IAuthenticate, AuthenticationService>();

            return services;
        }
    }
}
