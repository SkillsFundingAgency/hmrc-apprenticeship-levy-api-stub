using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.HMRC.API.Stub.Configuration.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Configuration
{
    /// <summary>
    /// Swagger configuration extensions
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Adds the swagger configuration to the <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "HMRC Stub API",
                    Version = "v1"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<AuthHeaderFilter>();
            });

            return services;
        }
    }
}
