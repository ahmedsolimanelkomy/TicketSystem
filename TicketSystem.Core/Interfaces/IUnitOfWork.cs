using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ITicketRepository Tickets { get; }
        Task<int> CompleteAsync();
    }
}
