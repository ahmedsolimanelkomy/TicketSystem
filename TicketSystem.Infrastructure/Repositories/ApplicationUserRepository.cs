using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Entities;
using TicketSystem.Infrastructure.Data;
using TicketSystem.Infrastructure.Identity;

namespace TicketSystem.Infrastructure.Repositories
{
    public class ApplicationUserRepository
    {
        private readonly TicketDbContext _context;

        public ApplicationUserRepository(TicketDbContext context)
        {
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _context.ApplicationUsers.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
