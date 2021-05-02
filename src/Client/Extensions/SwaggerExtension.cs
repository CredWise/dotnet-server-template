using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Sample.Client.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection service)
        {
            return service
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sample", Version = "v1" });
                });
        }

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
        {
            return app
                    .UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        c.DocumentTitle = "Sample Server";
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sample");
                    });
        }
    }
}