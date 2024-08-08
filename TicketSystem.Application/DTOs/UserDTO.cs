using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Application.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
    }
}
