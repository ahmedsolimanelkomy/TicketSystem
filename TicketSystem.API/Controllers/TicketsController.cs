using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Commands.CreateTicket;
using TicketSystem.Application.Queries.GetAllTickets;
using TicketSystem.Application.Queries.GetUserTicket;

namespace TicketSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketCommand command)
        {
            try
            {
                await _mediator.Send(command);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created();
        }

        [HttpGet("GetAllTickets")]
        public async Task<IActionResult> GetAllTickets()
        {
            var query = new GetAllTicketsQuery();
            var tickets = await _mediator.Send(query);
            return Ok(tickets);
        }
        [HttpGet("GetUserTickets")]
        public async Task<IActionResult> GetUserTickets([FromQuery] string mobileNumber)
        {
            var query = new GetUserTicketQuery(mobileNumber);
            var tickets = await _mediator.Send(query);
            if (tickets == null)
            {
                return NotFound();
            }
            return Ok(tickets);
        }
    }
}
