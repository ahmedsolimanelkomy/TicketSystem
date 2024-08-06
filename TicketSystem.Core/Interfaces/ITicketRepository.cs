using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Entities;

namespace TicketSystem.Core.Interfaces
{
    internal interface ITicketRepository : IRepository<Ticket>
    {
        Task<Ticket> GetByUserMobileNumberAsync(string mobileNumber);
    }
}
