using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
            if (user.Ticket != null) throw new Exception("User Already Has a Ticket");
            Ticket ticket = _mapper.Map<Ticket>(createTicketDto);
            ticket.TicketNumber = Guid.NewGuid().ToString();
            ticket.User = user;
            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<TicketDTO>> GetAllTicketsAsync()
        {
            IEnumerable<Ticket> Tickets = await _unitOfWork.Tickets.GetAllAsync();
            IEnumerable<TicketDTO> TicketsDto = _mapper.Map<IEnumerable<TicketDTO>>(Tickets);
            return TicketsDto;
        }

        public async Task<TicketDTO> GetUserTicketByMobileNumberAsync(string MobileNumber)
        {
            Ticket Tickets = await _unitOfWork.Tickets.GetAsync(Ticket => Ticket.User.MobileNumber == MobileNumber, ["User"]);
            TicketDTO TicketsDto = _mapper.Map<TicketDTO>(Tickets);
            return TicketsDto;
        }

        private string GenerateRandomNumber()
        {
            var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            var random = new Random();
            var randomNumber = random.Next(1000, 9999);
            string Number = $"{timestamp}-{randomNumber}";
            return Number;
        }
    }
}
