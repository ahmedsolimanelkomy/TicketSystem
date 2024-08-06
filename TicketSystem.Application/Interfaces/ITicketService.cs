using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;

namespace TicketSystem.Application.Interfaces
{
    public interface ITicketService
    {
        Task CreateTicketAsync(CreateTicketDTO createTicketDto);
        Task<IEnumerable<TicketDTO>> GetAllTicketsAsync();
        Task<IEnumerable<TicketDTO>> GetUserTicketByMobileNumberAsync(string MobileNumber);


    }
}
