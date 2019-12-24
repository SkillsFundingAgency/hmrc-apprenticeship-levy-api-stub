using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Configuration.Swagger
{
    /// <summary>
    /// Authentication header filter
    /// </summary>
    public class AuthHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// Adds an Authorization field to the Swagger document
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "authorization",
                In = "header",
                Type = "string",
                Required = false
            });
        }
    }
}
