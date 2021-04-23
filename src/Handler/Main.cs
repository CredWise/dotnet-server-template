using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Handler
{
    public static class Main
    {
        public static IServiceCollection AddHandler(this IServiceCollection services)
        {
            return services
                .AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}