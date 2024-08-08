using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Interfaces;
using TicketSystem.Infrastructure.Data;

namespace TicketSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }
        public ITicketRepository Tickets { get; private set; }

        private readonly TicketDbContext _context;

        public UnitOfWork(TicketDbContext context)
        {
            _context = context;
            Users = new UserRepository(context);
            Tickets = new TicketRepository(context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
