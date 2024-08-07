using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Core.Entities;

namespace TicketSystem.Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasher<User>
    {
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordHasherService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(User user, string password) => _passwordHasher.HashPassword(user, password);
        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword) =>
            _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
    }
}
