using CodingAssessment.Persistence;
using MediatR;

namespace CoddingAssesment.Application.Commands
{
    public class UpdateAgentRequest : IRequest<AgentState>
    {
        public Guid AgentId { get; set; }
        public string AgentName { get; set; } = null!;
        public DateTime TimeStampUtc { get; set; }
        public AgentActivity Action { get; set; }
        public Guid[] QueueIds { get; set; } = Array.Empty<Guid>();
    }

    public enum AgentActivity
    {
        CALL_STARTED = 1,
        START_DO_NOT_DISTURB = 2
    }
}
