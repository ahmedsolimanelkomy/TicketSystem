using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Application.DTOs
{
    public class LoginRequestDTO
    {
        [Required, MaxLength(30), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(30)]
        public string Password { get; set; }
    }
}
