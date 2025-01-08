using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Endpoint.Extentions
{
    public class GroupingByNamespaceConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace;
            var apiVersion = controllerNamespace.Split(".").Last().ToLower();

            if (!apiVersion.StartsWith("v")) { apiVersion = "v1"; }
            controller.ApiExplorer.GroupName = apiVersion;
        }
    }
    public static class SwaggerExtention
    {
        public static void SwaggerServiceBuild(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(new GroupingByNamespaceConvention());
            });

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "API v1", Version = "v1" });
                swagger.SwaggerDoc("v2", new OpenApiInfo { Title = "API v2", Version = "v2" });


                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}

                    }
                });

            });
        }

        public static void SwaggerAppBuild(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "API v2");
                    //c.DisplayOperationId();
                    //c.DisplayRequestDuration();
                });
            }

        }
    }

}
