using CodingAssessment.Persistence;
using MediatR;

namespace CoddingAssesment.Application.Queries
{
    public record GetAgentRequest : IRequest<IEnumerable<Agent>>;
}
