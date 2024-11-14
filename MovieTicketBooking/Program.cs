using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieTicketBooking.Hubs;
using MovieTicketBooking.Models;
using MovieTicketBooking.Services;
using System.Net;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace MovieTicketBooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddDbContext<MovieBookingContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

            builder.Services.AddScoped<MovieService>();
            builder.Services.AddScoped<ShowtimeService>();
            builder.Services.AddScoped<SeatService>();
            builder.Services.AddScoped<BookingService>();
            builder.Services.AddScoped<VnPayService>();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            var app = builder.Build();
            app.UseStaticFiles();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.MapRazorPages();
            app.MapHub<DocumentHub>("/documentHub");
            app.Run();
        }
    }
}