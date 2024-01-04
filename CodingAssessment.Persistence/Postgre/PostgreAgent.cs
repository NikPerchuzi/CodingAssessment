namespace CodingAssessment.Persistence.Postgre
{
    internal class PostgreAgent
    {
        public Guid AgentId { get; set; }
        public string AgentName { get; set; } = null!;
        public DateTime TimeStampUtc { get; set; }
        public AgentState State { get; set; }
        public ICollection<PostgreAgentSkill> AgentSkills { get; set; } = new List<PostgreAgentSkill>();
    }
}
