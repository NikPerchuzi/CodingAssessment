using Microsoft.EntityFrameworkCore;

namespace CodingAssessment.Persistence.Postgre
{
    [DatabaseType(DatabaseType.Postgre)]
    internal class AgentPostgreRepository : IAgentRepository
    {
        private readonly PostgreContext _context;
        public AgentPostgreRepository(PostgreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Agent>> GetAllAgentsAsync(CancellationToken token = default)
        {
            return await _context.Agents.Include(x => x.AgentSkills)
                                        .Select(x => new Agent(x.AgentId, x.AgentName, x.TimeStampUtc, x.State, x.AgentSkills.Select(y => y.SkillId).ToArray()))
                                        .ToListAsync(token);
        }

        public async Task UpdateAgentAsync(Agent agent, CancellationToken token = default)
        {
            var record = await _context.Agents.Include(x => x.AgentSkills).FirstOrDefaultAsync(x => x.AgentId == agent.AgentId, token);

            if (record is null)
            {
                throw new InvalidOperationException($"Cannot find agent by specified AgendId: {agent.AgentId}");
            }

            record.State = agent.State;
            record.TimeStampUtc = agent.TimeStampUtc;
            record.AgentSkills = agent.QueueIds.Select(skillId => new PostgreAgentSkill() { AgentId = agent.AgentId, SkillId = skillId }).ToList();

            _context.Agents.Update(record);
        }
    }
}
