using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Common.Behaviours;

namespace Sample.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
                    .AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}