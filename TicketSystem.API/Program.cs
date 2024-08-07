
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TicketSystem.Application.Commands.CreateTicket;
using TicketSystem.Application.Interfaces;
using TicketSystem.Application.Mappings;
using TicketSystem.Application.Queries.GetAllTickets;
using TicketSystem.Application.Queries.GetUserTicket;
using TicketSystem.Application.Services;
using TicketSystem.Core.Interfaces;
using TicketSystem.Infrastructure.Data;
using TicketSystem.Infrastructure.Identity;
using TicketSystem.Infrastructure.Repositories;

namespace TicketSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TicketDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("CS"),
                    sqlOptions => sqlOptions.MigrationsAssembly("TicketSystem.Infrastructure")));

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<TicketDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.AddTransient<RoleInitializer>();
            

            // Register MediatR
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(GetAllTicketsQueryHandler).Assembly,
                typeof(GetAllTicketsQuery).Assembly,
                typeof(GetUserTicketQueryHandler).Assembly,
                typeof(GetUserTicketQuery).Assembly,
                typeof(CreateTicketCommandHandler).Assembly,
                typeof(CreateTicketCommand).Assembly
                ));
            // Register other services
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
