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
        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
