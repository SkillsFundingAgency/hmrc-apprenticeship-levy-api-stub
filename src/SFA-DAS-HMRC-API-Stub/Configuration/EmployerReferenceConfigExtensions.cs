using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Data.Contexts;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    public static class EmployerReferenceConfig
    {
        public static IServiceCollection AddEmployerReference(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<GetEmployerReferenceRequest>();
            services.AddTransient<GetEmployerrReferenceResponse>();
            services.AddTransient<ICommand<GetEmployerReferenceRequest, GetEmployerrReferenceResponse>, GetEmployerReferenceCommand>();
            services.AddTransient<IEmployerReferenceRepository, EmployerReferenceRepository>();
            services.AddTransient<IEmployerReferenceDataContext, EmployerReferenceDataContext>();
            services.AddDbContext<EmployerReferenceDataContext>(options => options.UseSqlServer(config.GetConnectionString("default")));

            return services;
        }
    }
}
