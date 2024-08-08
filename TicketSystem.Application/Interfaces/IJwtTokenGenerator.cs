using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Entities;
using TicketSystem.Infrastructure.Identity;

namespace TicketSystem.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(ApplicationUser? user);
    }
}
