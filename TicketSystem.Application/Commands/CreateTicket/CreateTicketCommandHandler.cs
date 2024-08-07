using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;
using TicketSystem.Application.Interfaces;
using TicketSystem.Core.Entities;
using TicketSystem.Core.Interfaces;

namespace TicketSystem.Application.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
    {
        private readonly ITicketService _ticketService;

        public CreateTicketCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public async Task Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            CreateTicketDTO createTicketDTO = new CreateTicketDTO()
            {
                MobileNumber = request.MobileNumber,
                TicketImage = request.TicketImage,
            };
            await _ticketService.CreateTicketAsync(createTicketDTO);
        }
    }

}
