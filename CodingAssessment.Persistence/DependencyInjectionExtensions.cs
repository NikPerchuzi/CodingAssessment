using CodingAssessment.Persistence.Mongo;
using CodingAssessment.Persistence.Postgre;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CodingAssessment.Persistence
{
    public static class DependencyInjectionExtensions
    {
        private const string AgentMongoDatabase = nameof(AgentMongoDatabase);
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<DatabaseSelectorConfiguration>(configuration.GetSection(nameof(DatabaseSelectorConfiguration)))
                .AddScoped(x =>
                {
                    var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString(DatabaseType.Mongo.ToString())!);
                    var client = new MongoClient(settings);
                    return client.GetDatabase(AgentMongoDatabase);
                })
                .AddScoped<IAgentRepository, AgentMongoRepository>()
                .AddDbContext<PostgreContext>(options => options.UseNpgsql(configuration.GetConnectionString(DatabaseType.Postgre.ToString())!))
                .AddScoped<IAgentRepository, AgentPostgreRepository>()
                .Decorate<IAgentRepository>((_, provider) =>
                new AgentRepositoryProxy(provider.GetServices<IAgentRepository>(), provider.GetRequiredService<IOptionsSnapshot<DatabaseSelectorConfiguration>>()));
        }
    }
}
