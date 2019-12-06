using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Filters;
using SFA.DAS.HMRC.API.Stub.Services;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IAuthRecordRepository, AuthRecordRepository>();
            services.AddTransient<IAuthRecordDataContext, AuthRecordDataContext>();
            services.AddDbContext<AuthRecordDataContext>(options => options.UseSqlServer(config.GetConnectionString("default")));

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
