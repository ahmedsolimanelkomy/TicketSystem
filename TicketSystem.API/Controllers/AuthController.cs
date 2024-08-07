using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Commands.Login;
using TicketSystem.Application.Commands.Register;
using TicketSystem.Application.DTOs;
using TicketSystem.Application.Interfaces;

namespace TicketSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Please Enter Required Data" });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }

            try
            {
                var command = new RegisterCommand { Request = request };
                var result = await _mediator.Send(command);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "User registered successfully." });
                }

                return BadRequest(result.Errors);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request." });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Please Enter Required Data" });
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }

            try
            {
                var command = new LoginCommand { Request = request };
                var token = await _mediator.Send(command);

                if (token == null)
                {
                    return Unauthorized(new { Message = "Invalid Credentials" });
                }

                return Ok(new { Token = token });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request" });
            }
        }
    }
}
