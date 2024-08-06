using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Application.DTOs
{
    public class CreateTicketDTO
    {
        public string MobileNumber { get; set; }
        public string TicketImageUrl { get; set; }
    }
}
