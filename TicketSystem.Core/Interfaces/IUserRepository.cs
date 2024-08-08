using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Entities;

namespace TicketSystem.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByMobileNumberAsync(string mobileNumber);
    }
}
