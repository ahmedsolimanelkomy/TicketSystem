using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;
using TicketSystem.Application.Interfaces;

namespace TicketSystem.Application.Queries.GetUserTicket
{
    public class GetUserTicketQueryHandler : IRequestHandler<GetUserTicketQuery, TicketDTO>
    {
        private readonly ITicketService _ticketService;

        public GetUserTicketQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<TicketDTO> Handle(GetUserTicketQuery request, CancellationToken cancellationToken)
        {
            return await _ticketService.GetUserTicketByMobileNumberAsync(request.MobileNumber);
        }
    }
}
