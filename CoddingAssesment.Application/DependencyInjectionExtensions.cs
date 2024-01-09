using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoddingAssesment.Application
{
    public static class DependencyInjectionExtensions
    {
        private static Assembly ThisAssembly = typeof(DependencyInjectionExtensions).Assembly;

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddMediatR(ThisAssembly)
                           .AddTransient<IDateTimeProvider, DateTimeProvider>()
                           .Configure<TimeConfiguration>(configuration.GetSection(nameof(TimeConfiguration)));
        }
    }
}
