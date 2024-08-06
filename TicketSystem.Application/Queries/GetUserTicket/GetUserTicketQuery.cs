using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;

namespace TicketSystem.Application.Queries.GetUserTicket
{
    public class GetUserTicketQuery : IRequest<IEnumerable<TicketDTO>>
    {
        public string MobileNumber { get; }

        public GetUserTicketQuery(string mobileNumber)
        {
            MobileNumber = mobileNumber;
        }
    }
}
