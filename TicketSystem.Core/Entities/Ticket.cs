﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Core.Entities
{
    internal class Ticket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TicketNumber { get; set; }
        [DataType(DataType.ImageUrl)]
        public string TicketImage { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
