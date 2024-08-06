using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;

namespace TicketSystem.Application.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest
    {
        public CreateTicketDTO CreateTicketDto { get; }

        public CreateTicketCommand(CreateTicketDTO createTicketDto)
        {
            CreateTicketDto = createTicketDto;
        }
    }
}
