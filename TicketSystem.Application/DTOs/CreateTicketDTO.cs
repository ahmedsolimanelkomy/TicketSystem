using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Application.DTOs
{
    public class CreateTicketDTO
    {
        [Required, Phone]
        public string MobileNumber { get; set; }
        [Required]
        public IFormFile TicketImage { get; set; }
    }
}
