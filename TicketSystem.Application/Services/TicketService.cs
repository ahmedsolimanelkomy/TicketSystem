using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
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
            User user = await _unitOfWork.Users.GetByMobileNumberAsync(createTicketDto.MobileNumber)
                         ?? throw new KeyNotFoundException("User not found for the provided mobile number");

            if (user.Ticket != null)
            {
                throw new InvalidOperationException("The user already has an existing ticket");
            }

            Ticket ticket = _mapper.Map<Ticket>(createTicketDto);
            ticket.TicketNumber = Guid.NewGuid().ToString();
            ticket.UserId = user.Id;

            string imageName = await SaveTicketImageAsync(createTicketDto.TicketImage, ticket.TicketNumber);
            ticket.TicketImageUrl = imageName;

            await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<TicketDTO>> GetAllTicketsAsync()
        {
            try
            {
                IEnumerable<Ticket?> tickets = await _unitOfWork.Tickets.GetAllOrderedByTicketNumberDescAsync();

                if (tickets == null || !tickets.Any())
                {
                    return [];
                }
                IEnumerable<TicketDTO> ticketDTOs = _mapper.Map<IEnumerable<TicketDTO>>(tickets);
                return ticketDTOs;
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("Operation failed: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving tickets: " + ex.Message, ex);
            }
        }
        public async Task<TicketDTO?> GetUserTicketByMobileNumberAsync(string MobileNumber)
        {
                User? user = await _unitOfWork.Users.GetAsync(U => U.MobileNumber == MobileNumber) ?? throw new KeyNotFoundException("Invalid User Mobile Number");

                Ticket? Ticket = await _unitOfWork.Tickets.GetAsync(ticket => ticket.User.MobileNumber == MobileNumber, ["User"]);                
                
                if (Ticket == null)
                {
                    return null;
                }

                TicketDTO TicketsDto = _mapper.Map<TicketDTO>(Ticket);
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
        private static async Task<string> SaveTicketImageAsync(IFormFile ticketImage, string ticketNumber)
        {
            if (ticketImage == null || ticketNumber == null)
            {
                throw new ArgumentNullException(nameof(ticketImage), "Ticket image cannot be null");
            }

            try
            {
                var imageName = ticketNumber + ".jpeg";
                var imageFilePath = Path.Combine("wwwroot/TicketImages", imageName);

                using (var stream = new MemoryStream())
                {
                    await ticketImage.CopyToAsync(stream);
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

                return imageName;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the ticket image: " + ex.Message, ex);
            }
        }
    }
}
