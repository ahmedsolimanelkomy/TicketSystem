﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Core.Entities
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        [DataType(DataType.ImageUrl)]
        public string TicketImageUrl { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
