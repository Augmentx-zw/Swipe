using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;


namespace ArkPotal.PaymentService
{
    public static class Extension
    {
        public static void AddCommandQueryHandlers(this IServiceCollection services, Type handlerInterface, string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);

            var handlers = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            );

            foreach (var handler in handlers)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }
        }
    }
}
