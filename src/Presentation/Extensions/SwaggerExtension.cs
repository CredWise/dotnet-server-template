using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Presentation.Options;

namespace Presentation.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection service, IConfiguration configuration)
        {
            var swaggerOption = new SwaggerOption();
            configuration.GetSection(nameof(SwaggerOption)).Bind(swaggerOption);

            return service
                .AddSwaggerGen(c =>
                {
                    foreach (var version in swaggerOption.Versions) c.SwaggerDoc(version, new OpenApiInfo { Title = swaggerOption.Title, Version = version });

                    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                });
        }

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app, IConfiguration configuration)
        {
            var swaggerOption = new SwaggerOption();
            configuration.GetSection(nameof(SwaggerOption)).Bind(swaggerOption);

            if (!swaggerOption.ShowSwagger) return app;

            return app
                    .UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.DocumentTitle = swaggerOption.Title;
                        c.SwaggerEndpoint(swaggerOption.Endpoint, swaggerOption.Title);
                    });
        }
    }
}