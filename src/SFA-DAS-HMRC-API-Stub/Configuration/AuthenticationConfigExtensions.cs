using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Hosting;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Documents.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SFA.DAS.HMRC.API.Stub.Application.Commands;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo;
using SFA.DAS.HMRC.API.Stub.Domain;
using SFA.DAS.HMRC.API.Stub.Filters;
using SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth;
using SFA.DAS.HMRC.API.Stub.Services;
using System.Collections.Generic;
using System.Linq;

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
            var loginUrl = $"{config.GetValue<string>("taxService:url")}{config.GetValue<string>("taxService:signInPath")}?continue=/oauth/grantscope?auth_id={0}&origin=oauth-frontend";

            services.AddTransient<IAuthRecordRepository, AuthRecordRepository>(o =>
            {
                var logger = o.GetRequiredService<ILogger<AuthRecordRepository>>();
                var client = o.GetRequiredService<MongoClient>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new AuthRecordRepository(database, logger);
            });

            services.AddTransient<IAuthenticate, AuthenticationService>();
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = BearerAuthenticationOptions.DefaultScheme;
            })
            .AddCustomAuth(o => { });

            services.AddTransient<IApplicationRepository, ApplicationRepository>(o =>
            {
                var logger = o.GetRequiredService<ILogger<ApplicationRepository>>();
                var client = o.GetRequiredService<MongoClient>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new ApplicationRepository(database, logger);
            });

            services.AddTransient<IScopeRepository, ScopeRepository>(o =>
            {
                var logger = o.GetRequiredService<ILogger<ScopeRepository>>();
                var client = o.GetRequiredService<MongoClient>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new ScopeRepository(database, logger);
            });

            services.AddTransient<IAuthRequestRepository, AuthRequestRepository>(o =>
            {
                var logger = o.GetRequiredService<ILogger<AuthRequestRepository>>();
                var client = o.GetRequiredService<MongoClient>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new AuthRequestRepository(database, logger);
            });

            services.AddTransient<IAuthCodeRepository, AuthCodeRepository>(o =>
            {
                var client = o.GetRequiredService<MongoClient>();
                var logger = o.GetRequiredService<ILogger<AuthCodeRepository>>();
                var database = client.GetDatabase(config.GetValue<string>("mongoValues:databaseName"));

                return new AuthCodeRepository(database, logger);
            });

            services.AddTransient<ICommand<InsertAuthRequestRequest, InsertAuthRequestResponse>, InsertAuthRequestCommand>();
            services.AddTransient<IQuery<GetApplicationByIdRequest, GetApplicationByIdResponse>, GetApplicationsByIdQuery>();
            services.AddTransient<IQuery<GetScopeByNameRequest, GetScopeByNameResponse>, GetScopeByNameQuery>();
            services.AddTransient<IQuery<GetAuthCodeByCodeRequest, GetAuthCodeByCodeResponse>, GetAuthCodeByCodeQuery>();
            services.AddTransient<ICommand<DeleteAuthCodeByCodeRequest, DeleteAuthCodeByCodeResponse>, DeleteAuthCodeByCodeCommand>();
            services.AddTransient<ICommand<InsertAuthCodeRequest, InsertAuthCodeResponse>, InsertAuthCodeCommand>();
            services.AddTransient<IQuery<GetAllScopesRequest, GetAllScopesResponse>, GetAllScopesQuery>();
            services.AddTransient<ICommand<InsertAuthRecordRequest, InsertAuthRecordResponse>, InsertAuthRecordCommand>();
            services.AddTransient<IQuery<GetAuthRecordtByAccessTokenRequest, GetAuthRecordtByAccessTokenResponse>, GetAuthRecordtByAccessTokenQuery>();
            services.AddTransient<IQuery<GetAuthRecordtByRefreshTokenRequest, GetAuthRecordtByRefreshTokenResponse>, GetAuthRecordtByRefreshTokenQuery>();
            services.AddTransient<ICommand<UpdateAuthRecordRequest, UpdateAuthRecordResponse>, UpdateAuthRecordCommand>();
               services.AddIdentityServer(o =>
            {
                o.UserInteraction = new IdentityServer4.Configuration.UserInteractionOptions()
                {
                    LoginUrl = loginUrl
                };
            })
                .AddCustomAuthorizeRequestValidator<AuthValidator>()
                .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>()                     
                //change urls to match old api
                .Services.Where(service => service.ServiceType == typeof(Endpoint))
                        .Select(item => (Endpoint)item.ImplementationInstance)
                        .ToList()
                        .ForEach(item => item.Path = item.Path.Value.Replace("/connect", "/oauth"))
            ;

            services.AddTransient<IAuthorizationCodeStore, AuthorizationCodeStore>();
            services.AddTransient<ITokenCreationService, TokenCreationService>();
            services.AddTransient<ITokenResponseGenerator, Infrastructure.OAuth.TokenResponseGenerator>();
            services.AddTransient<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<ITokenValidator, TokenValidator>();
            return services;
        }
    }
}
