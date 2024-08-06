using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Core.Interfaces
{
    internal interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ITicketRepository Tickets { get; }
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveAsync();
    }
}
