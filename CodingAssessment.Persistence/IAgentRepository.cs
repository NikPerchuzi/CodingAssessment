namespace CodingAssessment.Persistence
{
    public interface IAgentRepository
    {
        public Task<IEnumerable<Agent>> GetAllAgentsAsync(CancellationToken token = default);
        public Task UpdateAgentAsync(Agent agent, CancellationToken token = default);
    }
}
