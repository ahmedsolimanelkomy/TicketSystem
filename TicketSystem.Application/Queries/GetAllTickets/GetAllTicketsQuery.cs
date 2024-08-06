﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;

namespace TicketSystem.Application.Queries.GetAllTickets
{
    public class GetAllTicketsQuery : IRequest<IEnumerable<TicketDTO>>
    {

    }
}
