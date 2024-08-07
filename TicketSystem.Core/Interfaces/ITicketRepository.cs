using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Entities;

namespace TicketSystem.Core.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<Ticket> GetByUserMobileNumberAsync(string mobileNumber);
        Task<IEnumerable<Ticket>> GetAllOrderedByTicketNumberDescAsync(string[]? includeProperties = null);

    }
}
