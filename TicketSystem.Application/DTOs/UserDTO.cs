using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Application.DTOs
{
    internal class UserDTO
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
    }
}
