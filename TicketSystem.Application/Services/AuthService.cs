using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.DTOs;
using TicketSystem.Application.Interfaces;
using TicketSystem.Infrastructure.Identity;

namespace TicketSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequestDTO request)
        {
            ApplicationUser user = new()
            {
                UserName = request.Email,
                Email = request.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return result;
        }

        public async Task<string?> AuthenticateAsync(LoginRequestDTO request)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);
                string token = await _jwtTokenGenerator.GenerateTokenAsync(user);
                return token;
            }

            return null;
        }
    }
}
