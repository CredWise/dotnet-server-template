using System;
using System.Collections.Generic;
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
            SwaggerOption swaggerOption = new();

            configuration.GetSection(nameof(SwaggerOption)).Bind(swaggerOption);

            service
               .AddSwaggerGen(c =>
               {
                   IList<string>? versions = swaggerOption.Versions;
                   if (versions != null && versions.Count > 0)
                       foreach (var version in versions) c.SwaggerDoc(version, new OpenApiInfo { Title = swaggerOption?.Title, Version = version });

                   c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
               });

            return service;
        }

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app, IConfiguration configuration)
        {
            SwaggerOption swaggerOption = new();
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