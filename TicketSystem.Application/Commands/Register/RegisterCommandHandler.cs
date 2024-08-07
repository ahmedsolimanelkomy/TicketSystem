using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.Interfaces;

namespace TicketSystem.Application.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, IdentityResult>
    {
        private readonly IAuthService _authService;

        public RegisterCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IdentityResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(command.Request);
        }
    }
}
