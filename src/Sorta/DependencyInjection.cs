using Microsoft.Extensions.DependencyInjection;
using Sorta.Abstractions;
using System.Linq;

namespace Sorta
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSortingAlgorithms(this IServiceCollection services)
        {
            var type = typeof(ISort);

            var sorts = type.Assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterface(nameof(ISort)) == type);

            foreach (var sort in sorts)
            {
                services.Add(new ServiceDescriptor(type, sort, ServiceLifetime.Singleton));
            }

            return services;
        }
    }
}
