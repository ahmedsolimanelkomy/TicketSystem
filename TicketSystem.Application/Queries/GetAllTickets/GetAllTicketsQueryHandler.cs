using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;
using TicketSystem.Application.Interfaces;
using TicketSystem.Core.Interfaces;

namespace TicketSystem.Application.Queries.GetAllTickets
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDTO>>
    {
        private readonly ITicketService _ticketService;

        public GetAllTicketsQueryHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task<IEnumerable<TicketDTO>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            return await _ticketService.GetAllTicketsAsync();
        }
    }

}
