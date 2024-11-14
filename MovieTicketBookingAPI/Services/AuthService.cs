using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Models;
using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace MovieTicketBookingAPI.Services
{
    public class AuthService
    {
        private readonly MovieBookingContext _context;
        private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        public AuthService(MovieBookingContext context)
        {
            _context = context;
        }

        public ApiResponse handleLogin(string email, string password)
        {
            var userInfo = _context.Users.SingleOrDefault(u => u.Email == email);
            if (userInfo == null ||
                passwordHasher.VerifyHashedPassword(userInfo, userInfo.Password, password) == PasswordVerificationResult.Failed)
            {
                return new ApiResponse { status = 400, data = "The email or password is incorrect!" };
            }

            switch (userInfo.Status)
            {
                case 0:
                    return new ApiResponse { status = 400, data = "You are banned!" };
                case -1:
                    return new ApiResponse { status = 400, data = "Your account has not been activated yet! Check your email to verify your account." };
            }

            return new ApiResponse { status = 200, data = JsonConvert.SerializeObject(userInfo) };
        }

        public string register(RegisterDTO register)
        {
            if (register.password != register.confirmPassword)
            {
                return "Confirm password does not match password!";
            }

            if (_context.Users.SingleOrDefault(u => u.Email == register.email) != null)
            {
                return "This email is already registered!";
            }

            User user = new User();

            user.Role = 1;
            user.Email = register.email;
            user.Name = register.name;
            user.Dob = register.birthDay;
            user.Status = -1;
            user.Password = passwordHasher.HashPassword(user, register.password);

            _context.Users.Add(user);
            _context.SaveChanges();
            return "Created user";
        }

        public string forgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "Please input your email first!";
            }

            var userInfo = _context.Users.SingleOrDefault(u => u.Email == email);
            if (userInfo == null)
            {
                return "This email is not registered!";
            }
            string newPassword = GenerateRandomPassword(10);
            userInfo.Password = passwordHasher.HashPassword(userInfo, newPassword);
            _context.SaveChanges();
            var emailBody = $@"
                <h2>Your new password</h2>
                <p>Your new password is: {newPassword}</p>";
            SendMailSMTP.SendMail(userInfo.Email, "Password Reset", emailBody);

            return "A new password has been sent to your email!";
        }

        private string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
