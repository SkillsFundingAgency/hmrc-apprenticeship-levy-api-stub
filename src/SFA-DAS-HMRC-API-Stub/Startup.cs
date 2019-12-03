using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Filters;
using SFA.DAS.HMRC.API.Stub.Repositories;
using SFA.DAS.HMRC.API.Stub.Services;

namespace SFA_DAS_HMRC_API_Stub
{
    public class Startup
    {
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

            var config = AddConfig();
            services.AddTransient<GetEmployerChecksRequest>();
            services.AddTransient<GetEmployerChecksResponse>();
            services.AddTransient<ICommand<GetEmployerChecksRequest, GetEmployerChecksResponse>, GetEmployerChecksCommand>();
            services.AddTransient<IEmployerChecksRepository, EmployerChecksRepository>();
            services.AddTransient<IEmployerDataContext, EmployerDataContext>();
            services.AddDbContext<EmployerDataContext>(options => options.UseSqlServer(config.GetConnectionString("default")));

            services.AddTransient<GetEmployerReferenceRequest>();
            services.AddTransient<GetEmployerrReferenceResponse>();
            services.AddTransient<ICommand<GetEmployerReferenceRequest, GetEmployerrReferenceResponse>, GetEmployerReferenceCommand>();
            services.AddTransient<IEmployerReferenceRepository, EmployerReferenceRepository>();
            services.AddTransient<IEmployerReferenceDataContext, EmployerReferenceDataContext>();
            services.AddDbContext<EmployerReferenceDataContext>(options => options.UseSqlServer(config.GetConnectionString("default")));

            services.AddTransient<IAuthRecordRepository, AuthRecordRepository>();
            services.AddTransient<IAuthRecordDataContext, AuthRecordDataContext>();
            services.AddDbContext<AuthRecordDataContext>(options => options.UseSqlServer(config.GetConnectionString("default")));

            services.AddTransient<IGatewayRepository, GatewayRepository>();
            services.AddTransient<IGatewayDataContext, GatewayDataContext>();
            services.AddDbContext<GatewayDataContext>(options => options.UseSqlServer(config.GetConnectionString("default")));

            services.AddTransient<IAuthenticate, AuthenticationService>();
            services.AddScoped<AuthorisationFilter>();
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultScheme = BearerAuthenticationOptions.DefaultScheme;
                cfg.DefaultChallengeScheme = BearerAuthenticationOptions.DefaultScheme;
            })
            .AddCustomAuth(o => { });

            services.AddLogging(configure =>
            {
                configure.AddConsole();
            });
        }

        private IConfiguration AddConfig()
        {
            return new ConfigurationBuilder()
                .AddConfiguration(Configuration)
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .AddJsonFile("appSettings.Development.json", true)
                .AddEnvironmentVariables()
                .Build()
            ;
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

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
