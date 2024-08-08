using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Commands.CreateTicket;
using TicketSystem.Application.Queries.GetAllTickets;
using TicketSystem.Application.Queries.GetUserTicket;

namespace TicketSystem.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddTicket")]
        public async Task<IActionResult> Create([FromForm] CreateTicketCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _mediator.Send(command);
                return CreatedAtAction(nameof(GetUserTicket), new { mobileNumber = command.MobileNumber }, command);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex?.InnerException?.Message });
            }
        }

        [HttpGet("GetAllTicketsDesc")]
        public async Task<IActionResult> GetAllTicketsDesc()
        {
            try
            {
                var query = new GetAllTicketsQuery();
                var tickets = await _mediator.Send(query);
                if (tickets == null || !tickets.Any())
                {
                    return NotFound(new { Message = "No tickets found." });
                }
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving tickets.", Details = ex.Message });
            }
        }

        [HttpGet("GetUserTicket")]
        public async Task<IActionResult> GetUserTicket([FromQuery] string mobileNumber)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber))
            {
                return BadRequest(new { Message = "Mobile number is required" });
            }

            try
            {
                var query = new GetUserTicketQuery(mobileNumber);
                var ticket = await _mediator.Send(query);
                if (ticket == null)
                {
                    return NotFound(new { Message = "Ticket not found for the provided mobile number" });
                }
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the ticket", Details = ex.Message });
            }
        }
    }
}
