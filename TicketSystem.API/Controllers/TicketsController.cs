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
                return CreatedAtAction(nameof(GetUserTicketByMobileNumber), new { mobileNumber = command.MobileNumber }, command);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the ticket.", Details = ex.Message });
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
                    return NotFound(new { Message = "No tickets found" });
                }
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving tickets.", Details = ex.Message });
            }
        }

        [HttpGet("GetUserTicketByMobileNumber")]
        public async Task<IActionResult> GetUserTicketByMobileNumber([FromQuery] string mobileNumber)
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
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving the ticket", Details = ex.Message });
            }
        }
    }
}
