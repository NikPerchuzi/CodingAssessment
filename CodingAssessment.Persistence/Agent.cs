using MongoDB.Bson.Serialization.Attributes;

namespace CodingAssessment.Persistence
{
    public sealed class Agent
    {
        public Agent(Guid agentId, string agentName, DateTime timeStampUtc, AgentState state, Guid[] queueIds)
        {
            AgentId = agentId;
            AgentName = agentName;
            TimeStampUtc = timeStampUtc;
            State = state;
            QueueIds = queueIds;
        }

        [BsonId]
        public Guid AgentId { get; set; }
        public string AgentName { get; set; } = null!;
        public DateTime TimeStampUtc { get; set; }
        public AgentState State { get; set; }
        public Guid[] QueueIds { get; set; } = Array.Empty<Guid>();
    }

    public enum AgentState
    {
        ON_LUNCH,
        ON_CALL
    }
}
