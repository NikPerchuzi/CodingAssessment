using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

namespace CodingAssessment.Persistence
{
    internal sealed class AgentRepositoryProxy : IAgentRepository
    {
        private readonly IAgentRepository _repository;

        public AgentRepositoryProxy(IEnumerable<IAgentRepository> repositories, IOptionsSnapshot<DatabaseSelectorConfiguration> options)
        {
            _repository = repositories.First(x => x.GetType().GetCustomAttribute<DatabaseTypeAttribute>()?.Type == options.Value.DatabaseType);
        }

        public Task<IEnumerable<Agent>> GetAllAgentsAsync(CancellationToken token = default) => _repository.GetAllAgentsAsync(token);

        public Task UpdateAgentAsync(Agent agent, CancellationToken token = default) => _repository.UpdateAgentAsync(agent, token);
    }
}
