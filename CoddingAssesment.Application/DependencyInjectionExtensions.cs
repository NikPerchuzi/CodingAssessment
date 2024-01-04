using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoddingAssesment.Application
{
    public static class DependencyInjectionExtensions
    {
        private static Assembly ThisAssembly = typeof(DependencyInjectionExtensions).Assembly;

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services.AddMediatR(ThisAssembly);
        }
    }
}
