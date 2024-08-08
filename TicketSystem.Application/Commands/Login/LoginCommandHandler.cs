using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.Interfaces;

namespace TicketSystem.Application.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string?>
    {
        private readonly IAuthService _authService;

        public LoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<string?> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            return await _authService.AuthenticateAsync(command.Request);
        }
    }
}
