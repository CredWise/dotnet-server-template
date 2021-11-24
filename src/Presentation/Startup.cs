using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plutus.Utility;
using Presentation.Extensions;

namespace Presentation;
public class Startup
{
    private IConfiguration _config { get; }

    public Startup(IConfiguration configuration)
    {
        _config = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

        services.AddControllers(x =>
                {
                    x.Filters.Add<ValidationFilter>();
                })
                .AddJsonOptions();

        services.AddSwagger(_config)
                .AddAPIVersioning()
                .AddApplication(_config)
                .AddInfrastructure(_config);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwaggerMiddleware(_config)
           .UseForwardedHeaders(new ForwardedHeadersOptions
           {
               ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
           })
           .UseCors()
           .UseCustomExceptionHandler()
           .UseRouting()
           .UseAuthorization()
           .UseEndpoints(endpoints =>
           {
               endpoints.MapControllers();
           });
    }
}
