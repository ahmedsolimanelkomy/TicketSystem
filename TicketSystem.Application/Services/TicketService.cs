using AutoMapper;
using Microsoft.AspNetCore.Http;
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
using static System.Net.Mime.MediaTypeNames;

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

            var ticket = _mapper.Map<Ticket>(createTicketDto);
            ticket.TicketNumber = Guid.NewGuid().ToString();
            ticket.UserId = user.Id;

            var imageFilePath = Path.Combine("wwwroot/TicketImages", ticket.TicketNumber + ".Jpeg");

            using (var stream = new MemoryStream())
            {
                await createTicketDto.TicketImage.CopyToAsync(stream);
                using (var image = System.Drawing.Image.FromStream(stream))
                {
                    var jpegEncoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
                    var encoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                    encoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90L); // Adjust quality as needed

                    using (var fileStream = new FileStream(imageFilePath, FileMode.Create))
                    {
                        image.Save(fileStream, jpegEncoder, encoderParameters);
                    }
                }
            }

            ticket.TicketImageUrl = imageFilePath;

            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<TicketDTO>> GetAllTicketsAsync()
        {
            IEnumerable<Ticket> Tickets = await _unitOfWork.Tickets.GetAllOrderedByTicketNumberDescAsync();
            IEnumerable<TicketDTO> TicketsDto = _mapper.Map<IEnumerable<TicketDTO>>(Tickets);
            return TicketsDto;
        }

        public async Task<TicketDTO> GetUserTicketByMobileNumberAsync(string MobileNumber)
        {
            Ticket Tickets = await _unitOfWork.Tickets.GetAsync(Ticket => Ticket.User.MobileNumber == MobileNumber, ["User"]);
            TicketDTO TicketsDto = _mapper.Map<TicketDTO>(Tickets);
            return TicketsDto;
        }

        private static System.Drawing.Imaging.ImageCodecInfo GetEncoder(System.Drawing.Imaging.ImageFormat format)
        {
            var codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
