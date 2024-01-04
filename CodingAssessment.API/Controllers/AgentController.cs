using CoddingAssesment.Application.Commands;
using CoddingAssesment.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodingAssessment.API.Controllers
{
    [ApiController]
    [Route("agent")]
    public class AgentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AgentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAgentRequest request, CancellationToken cancellationToken = default)
        {
            var agents = await _mediator.Send(request, cancellationToken);

            return Ok(agents);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAgentRequest request, CancellationToken cancellationToken = default)
        {
            var state = await _mediator.Send(request, cancellationToken);

            return Ok(state);
        }

    }
}
