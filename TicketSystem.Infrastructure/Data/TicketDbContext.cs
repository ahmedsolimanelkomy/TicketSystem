using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Entities;
using TicketSystem.Infrastructure.Identity;

namespace TicketSystem.Infrastructure.Data
{
    public class TicketDbContext(DbContextOptions<TicketDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
    {
        public new DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "ahmed", MobileNumber = "123-456-7890" },
                new User { Id = 2, Name = "mahmoud", MobileNumber = "234-567-8901" },
                new User { Id = 3, Name = "Ali", MobileNumber = "345-678-9012" },
                new User { Id = 4, Name = "mostafa", MobileNumber = "456-789-0123" },
                new User { Id = 5, Name = "kareem", MobileNumber = "567-890-1234" },
                new User { Id = 6, Name = "dina", MobileNumber = "678-901-2345" },
                new User { Id = 7, Name = "yara", MobileNumber = "789-012-3456" },
                new User { Id = 8, Name = "nada", MobileNumber = "890-123-4567" },
                new User { Id = 9, Name = "sara", MobileNumber = "901-234-5678" },
                new User { Id = 10, Name = "yasmeen", MobileNumber = "012-345-6789" }
            );
        }
    }
}
