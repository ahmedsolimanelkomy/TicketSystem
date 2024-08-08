using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.Interfaces;
using TicketSystem.Infrastructure.Identity;

namespace TicketSystem.Application.Services
{
    public class JwtTokenGenerator(IConfiguration configuration, UserManager<ApplicationUser> userManager) : IJwtTokenGenerator
    {
        private readonly string? _secretKey = configuration["JwtSettings:SecretKey"];
        private readonly string? _issuer = configuration["JwtSettings:Issuer"];
        private readonly string? _audience = configuration["JwtSettings:Audience"];

        public async Task<string> GenerateTokenAsync(ApplicationUser? user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey!);

            var roles = await userManager.GetRolesAsync(user!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user?.UserName!),
                new Claim(ClaimTypes.Email, user?.Email!),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}