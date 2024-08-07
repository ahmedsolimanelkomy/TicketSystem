using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TicketSystem.Application.DTOs;
using AutoMapper;
using TicketSystem.Core.Entities;

namespace TicketSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
            public MappingProfile()
            {
                CreateMap<User, UserDTO>().ReverseMap();
                CreateMap<Ticket, TicketDTO>().ReverseMap();
                CreateMap<CreateTicketDTO, Ticket>().ReverseMap();
            }
    }
}
