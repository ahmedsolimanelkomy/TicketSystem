using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Entities;
using TicketSystem.Core.Interfaces;
using TicketSystem.Infrastructure.Data;
using TicketSystem.Infrastructure.Migrations;

namespace TicketSystem.Infrastructure.Repositories
{
    public class TicketRepository(TicketDbContext context) : Repository<Ticket>(context), ITicketRepository
    {
        public async Task<IEnumerable<Ticket>> GetAllOrderedByTicketNumberDescAsync(string[]? includeProperties = null)
        {
            IQueryable<Ticket> query = _context.Tickets.OrderByDescending(t => t.TicketNumber);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<Ticket> GetByUserMobileNumberAsync(string mobileNumber)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber))
            {
                throw new ArgumentException("Mobile number cannot be null or empty.", nameof(mobileNumber));
            }

            var ticket = await _context.Tickets
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.User.MobileNumber == mobileNumber);

            if (ticket == null)
            {
                return null;
            }
            return ticket;
        }

    }
}
