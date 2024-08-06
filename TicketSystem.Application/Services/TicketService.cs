using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;
using TicketSystem.Application.Interfaces;
using TicketSystem.Core.Entities;
using TicketSystem.Core.Interfaces;

namespace TicketSystem.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateTicketAsync(CreateTicketDTO createTicketDto)
        {
            User user = await _unitOfWork.Users.GetByMobileNumberAsync(createTicketDto.MobileNumber);
            if (user == null) throw new Exception("User not found");

            Ticket ticket = _mapper.Map<Ticket>(createTicketDto);
    
            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<TicketDTO>> GetAllTicketsAsync()
        {
            IEnumerable<Ticket> Tickets = await _unitOfWork.Tickets.GetAllAsync();
            IEnumerable<TicketDTO> TicketsDto = _mapper.Map<IEnumerable<TicketDTO>>(Tickets);
            return TicketsDto;
        }

        public async Task<IEnumerable<TicketDTO>> GetUserTicketByMobileNumberAsync(string MobileNumber)
        {
            IEnumerable<Ticket> Tickets = await _unitOfWork.Tickets.GetListAsync(Ticket => Ticket.User.MobileNumber == MobileNumber, ["User"]);
            IEnumerable<TicketDTO> TicketsDto = _mapper.Map<IEnumerable<TicketDTO>>(Tickets);
            return TicketsDto;
        }
    }
}
