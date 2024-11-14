using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Pages.Auth
{
    public class VerifyAccountModel : PageModel
    {
        private readonly MovieBookingContext context = new MovieBookingContext();
        public VerifyAccountModel(MovieBookingContext _context)
        {
            context = _context;
        }
        public string Message { get; set; }
        public IActionResult OnGet(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                Message = "Invalid email!";
                return Page();
            }

            var user = context.Users.SingleOrDefault(u => u.Email == email);

            if (user == null)
            {
                Message = "Account not found!";
                return Page();
            }

            if (user.Status == -1)
            {
                user.Status = 1;
                context.SaveChanges();
                Message = "Account activated successfully!";
            }
            else
            {
                Message = "Account is already active!";
            }

            return Page();
        }
    }
}
