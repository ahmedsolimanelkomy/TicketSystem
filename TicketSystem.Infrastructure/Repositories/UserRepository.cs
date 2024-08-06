using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Entities;
using TicketSystem.Core.Interfaces;
using TicketSystem.Infrastructure.Data;

namespace TicketSystem.Infrastructure.Repositories
{
    public class UserRepository(TicketDbContext context) : Repository<User>(context), IUserRepository
    {
        public async Task<User> GetByMobileNumberAsync(string mobileNumber)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber))
            {
                throw new ArgumentException("Mobile number cannot be null or empty.", nameof(mobileNumber));
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.MobileNumber == mobileNumber);

            return user;
        }

    }
}
