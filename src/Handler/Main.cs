using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Plutus.Utility.Extension;

namespace Sample.Handler
{
    public static class Main
    {
        public static IServiceCollection AddHandler(this IServiceCollection services)
        {
            return services
                .AddAPIVersioning()
                .AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}