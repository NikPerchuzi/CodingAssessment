using MongoDB.Driver;

namespace CodingAssessment.Persistence.Mongo
{
    [DatabaseType(DatabaseType.Mongo)]
    internal sealed class AgentMongoRepository : IAgentRepository
    {
        private const string Agents = nameof(Agents);

        private readonly IMongoCollection<Agent> _collection;

        public AgentMongoRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Agent>(Agents);
        }

        public async Task<IEnumerable<Agent>> GetAllAgentsAsync(CancellationToken token = default)
        {
            return await _collection.Find(_ => true).ToListAsync(token);
        }

        public async Task UpdateAgentAsync(Agent agent, CancellationToken token = default)
        {
            var update = Builders<Agent>.Update.Set(record => record.State, agent.State)
                                               .Set(record => record.AgentName, agent.AgentName)
                                               .Set(record => record.TimeStampUtc, agent.TimeStampUtc)
                                               .Set(record => record.QueueIds, agent.QueueIds);

            await _collection.UpdateOneAsync(x => x.AgentId == agent.AgentId, update, new UpdateOptions { IsUpsert = true }, cancellationToken: token);
        }
    }
}
