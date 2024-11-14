using Microsoft.AspNetCore.Identity;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Models;

namespace MovieTicketBookingAPI.Services
{
    public class UserService
    {
        private readonly MovieBookingContext _context;
        private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        public UserService(MovieBookingContext context)
        {
            _context = context;
        }

        public User editProfile(int userId, EditProfileDTO user)
        {
            User oldU = _context.Users.FirstOrDefault(x => x.UserId == userId);
            oldU.Name = user.name;
            oldU.Phone = user.phone;
            oldU.Dob = user.dob;
            _context.SaveChanges();
            return oldU;
        }

        public ApiResponse changePassword(int userId, ChangePasswordDTO request)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, request.currentPassword);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return new ApiResponse
                {
                    status = 400,
                    data = "Current password is incorrect."
                };
            }

            if (request.newPassword != request.rePassword)
            {
                return new ApiResponse
                {
                    status = 400,
                    data = "New password and confirmation does not match."
                };
            }
            user.Password = passwordHasher.HashPassword(user, request.newPassword);
            _context.SaveChanges();
            return new ApiResponse
            {
                status = 200,
                data = "Password changed successfully!"
            };
        }
    }
}
