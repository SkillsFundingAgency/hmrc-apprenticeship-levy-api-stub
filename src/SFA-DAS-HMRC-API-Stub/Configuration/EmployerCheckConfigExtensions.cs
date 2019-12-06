using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Repositories;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class EmployerCheckConfig
    {
        public static IServiceCollection AddEmployerChecks(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetEmployerChecksRequest>();
            services.AddTransient<GetEmployerChecksResponse>();
            services.AddTransient<ICommand<GetEmployerChecksRequest, GetEmployerChecksResponse>, GetEmployerChecksCommand>();
            services.AddTransient<IEmployerChecksRepository, EmployerChecksRepository>();
            services.AddTransient<IEmployerDataContext, EmployerDataContext>();
            services.AddDbContext<EmployerDataContext>(options => options.UseSqlServer(config.GetConnectionString("default")));

            return services;
        }
    }
}
