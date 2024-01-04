using CodingAssessment.Persistence;
using MediatR;

namespace CoddingAssesment.Application.Commands
{
    internal class UpdateAgentHandler : IRequestHandler<UpdateAgentRequest, AgentState>
    {
        // TODO: get from config; 
        private readonly TimeSpan _startDate = TimeOnly.Parse("11:00 AM").ToTimeSpan();
        private readonly TimeSpan _endDate = TimeOnly.Parse("01:00 PM").ToTimeSpan();
        private readonly TimeSpan _allowableInterval = TimeSpan.FromHours(1);

        // TODO: get from property; 
        private readonly DateTime CurrentDate = DateTime.UtcNow;
        private IAgentRepository _repository;

        public UpdateAgentHandler(IAgentRepository repository)
        {
            _repository = repository;
        }

        public async Task<AgentState> Handle(UpdateAgentRequest request, CancellationToken cancellationToken)
        {
            var state = DetermineAgentState(request);
            var agent = new Agent(request.AgentId, request.AgentName, request.TimeStampUtc, state, request.QueueIds);
            await _repository.UpdateAgentAsync(agent, cancellationToken);

            return state;
        }

        private AgentState DetermineAgentState(UpdateAgentRequest request)
        {
            var interval = (request.TimeStampUtc - CurrentDate).Duration();
            if (interval > _allowableInterval)
            {
                throw new LateEventException(interval);
            }

            if (request.Action == AgentActivity.CALL_STARTED)
            {
                return AgentState.ON_CALL;
            }

            if (request.Action == AgentActivity.START_DO_NOT_DISTURB)
            {
                return OnLunch(request);
            }

            throw new InvalidOperationException("Unexpected agent action defined");
        }

        private AgentState OnLunch(UpdateAgentRequest request)
        {
            var agentTime = request.TimeStampUtc.TimeOfDay;
            if (agentTime < _startDate && agentTime > _endDate)
            {
                throw new NotLunchTimeException(agentTime);
            }

            return AgentState.ON_LUNCH;
        }
    }
}
