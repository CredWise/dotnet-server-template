using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sample.Application.Behaviours;

namespace Sample.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                    .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>))
                    .AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}