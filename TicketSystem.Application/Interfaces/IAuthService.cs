using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;

namespace TicketSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequestDTO request);
        Task<string?> AuthenticateAsync(LoginRequestDTO request);
    }
}
