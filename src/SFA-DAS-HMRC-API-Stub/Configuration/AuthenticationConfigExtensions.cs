﻿using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Filters;
using SFA.DAS.HMRC.API.Stub.Services;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    /// <summary>
    /// Authentication configuration <see cref="IServiceCollection"/> extensions
    /// </summary>
    public static class AuthenticationConfig
    {
        /// <summary>
        /// Adds authentication services to the <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IAuthRecordRepository, AuthRecordCosmosRepository>(o =>
            {
                var client = o.GetRequiredService<DocumentClient>();
                var logger = o.GetRequiredService<ILogger<AuthRecordCosmosRepository>>();
                var collectionUri = UriFactory.CreateDocumentCollectionUri(config.GetValue<string>("cosmosValues:databaseName"), Constants.AUTHRECORDS);

                return new AuthRecordCosmosRepository(client, logger, collectionUri);
            });

            services.AddTransient<IAuthenticate, AuthenticationService>();
            services.AddScoped<AuthorisationFilter>();
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultScheme = BearerAuthenticationOptions.DefaultScheme;
                cfg.DefaultChallengeScheme = BearerAuthenticationOptions.DefaultScheme;
            })
            .AddCustomAuth(o => { });

            return services;
        }
    }
}
