using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class LevyDeclarationConfig
    {
        public static IServiceCollection AddLevyDeclaration(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetLevyDeclarationRequest>();
            services.AddTransient<GetLevyDeclarationResponse>();
            services.AddTransient<ICommand<GetLevyDeclarationRequest, GetLevyDeclarationResponse>, GetLevyDeclarationCommand>();
            services.AddTransient<ILevyDeclarationRepository, LevyDeclarationRepository>();
            services.AddTransient<ILevyDeclarationDataContext, LevyDeclarationDataContext>();
            services.AddDbContext<LevyDeclarationDataContext>(options => options.UseSqlServer(config.GetConnectionString("default")));

            return services;
        }
    }
}
