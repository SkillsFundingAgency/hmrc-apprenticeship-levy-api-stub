﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Configuration;
using System;
using NLog.Extensions.Logging;
using SFA.DAS.HMRC.API.Stub.Infrastructure;
using MongoDB.Driver;

namespace SFA_DAS_HMRC_API_Stub
{
    public class Startup
    {
        private MongoClient _client;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opts =>
            {
                opts.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build()));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var config = new ConfigurationBuilder()
                .AddConfig(Configuration);

            _client = new MongoClient(config.GetValue<string>("mongoValues:databaseUri"));

            services.AddSingleton<MongoClient>(o =>
            {
                return _client;
            });

            services
                .AddEmployerChecks(config)
                .AddEmployerReference(config)
                .AddLevyDeclaration(config)
                .AddFractions(config)
                .AddFractionCalcDate(config)
                .AddAuthentication(config)
                .AddGatewayUsers(config)
                .AddSwagger()
            ;

            var nLogConfiguration = new NLogConfiguration();
            services.AddLogging(options =>
            {
                options.AddNLog(new NLogProviderOptions
                {
                    CaptureMessageTemplates = true,
                    CaptureMessageProperties = true
                });
                options.AddConsole();

                nLogConfiguration.ConfigureNLog(config);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseIdentityServer();
            //app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HMRC Stub v1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
