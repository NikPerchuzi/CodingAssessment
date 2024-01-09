using CodingAssessment.Persistence;
using MediatR;
using Microsoft.Extensions.Options;

namespace CoddingAssesment.Application.Commands
{
    internal class UpdateAgentHandler : IRequestHandler<UpdateAgentRequest, AgentState>
    {
        private readonly IAgentRepository _repository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IOptionsSnapshot<TimeConfiguration> _options;

        public UpdateAgentHandler(IAgentRepository repository, IDateTimeProvider dateTimeProvider, IOptionsSnapshot<TimeConfiguration> optionsSnapshot)
        {
            _repository = repository;
            _dateTimeProvider = dateTimeProvider;
            _options = optionsSnapshot;
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
            var interval = (request.TimeStampUtc - _dateTimeProvider.UtcNow).Duration();
            if (interval > _options.Value.AllowableInterval)
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
            if (agentTime < _options.Value.StartDate || agentTime > _options.Value.EndDate)
            {
                throw new NotLunchTimeException(agentTime);
            }

            return AgentState.ON_LUNCH;
        }
    }
}
