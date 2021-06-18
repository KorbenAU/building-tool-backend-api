using System.Linq;
using Microservice.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microservice.API.Filters
{
    public class RequiresAdditionalHeaders : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var globalAttributes = context.ApiDescription.ActionDescriptor.FilterDescriptors.Select(p => p.Filter);
            var controlerAttributes = context.MethodInfo?.DeclaringType?.GetCustomAttributes(true);
            var methodAttributes = context.MethodInfo?.GetCustomAttributes(true);
            var produceAttributes = globalAttributes
                .Union(controlerAttributes)
                .Union(methodAttributes);

            if (produceAttributes.OfType<RequireSessionAttribute>().Any())
                operation.Parameters?.Add(new OpenApiParameter
                {
                    Name = Constants.SessionHeaderName,
                    In = ParameterLocation.Header,
                    Description = "Session token",
                    Required = true,
                    Schema = new OpenApiSchema {Type = "string"}
                });
        }
    }

    public class RequiresSessionHeaderAttribute : TypeFilterAttribute
    {
        public RequiresSessionHeaderAttribute()
            : base(typeof(RequiresAdditionalHeaders))
        {
        }
    }
}