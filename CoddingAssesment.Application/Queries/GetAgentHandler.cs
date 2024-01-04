using CodingAssessment.Persistence;
using MediatR;

namespace CoddingAssesment.Application.Queries
{
    internal class GetAgentHandler : IRequestHandler<GetAgentRequest, IEnumerable<Agent>>
    {
        private IAgentRepository _repository;

        public GetAgentHandler(IAgentRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Agent>> Handle(GetAgentRequest request, CancellationToken cancellationToken)
        {
            return _repository.GetAllAgentsAsync(cancellationToken);
        }
    }
}
