using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;

namespace TicketSystem.Application.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest <CreateTicketDTO>
    {
        public required string MobileNumber { get; set; }
        public required IFormFile TicketImage { get; set; }

        public CreateTicketCommand() { }

        public CreateTicketCommand(string mobileNumber, IFormFile ticketImage)
        {
            MobileNumber = mobileNumber;
            TicketImage = ticketImage;
        }
    }
}
