using System.Reflection;
using Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceAndIpBehaviour<,>))
                    .AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}