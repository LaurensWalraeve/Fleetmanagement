using FleetManagement.API.CreationModels;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FleetManagement.API.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerExamplesFromAssemblyOf<DriverUpdateModelExample>();
            services.AddSwaggerGen(s =>
            {
                // Add this line to enable example values in the Swagger documentation
                s.ExampleFilters();

                s.AddSecurityDefinition("Bearer JWT", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "FleetManagement API v1");
                s.RoutePrefix = string.Empty;
            });
        }
    }
}