namespace Boovey.Web.Extensions
{
    using System.Collections.Generic;
    using Microsoft.OpenApi.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Constants;

    public static class SwaggerConfigurator
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(SwaggerConfigValues.SwaggerDocVersion, new OpenApiInfo
                {
                    Title = SwaggerConfigValues.OpenApiInfoTitle,
                    Version = SwaggerConfigValues.OpenApiInfoVersion,
                    Description = SwaggerConfigValues.OpenApiInfoDescription
                });

                // Adds the authorize button in swagger UI 
                c.AddSecurityDefinition(SwaggerConfigValues.SecurityDefinitionName, new OpenApiSecurityScheme
                {
                    Description = SwaggerConfigValues.OpenApiSecuritySchemeDescription,
                    Name = SwaggerConfigValues.OpenApiSecuritySchemeName,
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                // Uses the token from the authorize input and sends it as a header
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                            {
                            Type = ReferenceType.SecurityScheme,
                            Id = SwaggerConfigValues.OpenApiId
                            },
                            Scheme = SwaggerConfigValues.OpenApiScheme,
                            Name = SwaggerConfigValues.OpenApiName,
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
