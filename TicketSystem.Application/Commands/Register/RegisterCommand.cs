using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;

namespace TicketSystem.Application.Commands.Register
{
    public class RegisterCommand : IRequest<IdentityResult>
    {
        public required RegisterRequestDTO Request { get; set; }
    }
}
