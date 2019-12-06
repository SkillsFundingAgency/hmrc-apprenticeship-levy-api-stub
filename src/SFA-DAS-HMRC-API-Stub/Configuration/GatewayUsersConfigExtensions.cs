using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class GatewayUsersConfig
    {
        public static IServiceCollection AddGatewayUsers(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IGatewayRepository, GatewayRepository>();
            services.AddTransient<IGatewayDataContext, GatewayDataContext>();
            services.AddDbContext<GatewayDataContext>(options => options.UseSqlServer(config.GetConnectionString("default")));

            return services;
        }
    }
}
