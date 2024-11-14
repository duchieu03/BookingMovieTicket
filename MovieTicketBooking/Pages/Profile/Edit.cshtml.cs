using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace MovieTicketBooking.Pages.Profile
{
    public class EditModel : PageModel
    {
        public string Message { get; set; }
        [BindProperty]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public User User { get; set; }
        private HttpClient httpClient;
        public EditModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public IActionResult OnGet()
        {
            var userJson = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Auth/Login");
            }
            var user = JsonConvert.DeserializeObject<User>(userJson);
            User u = user;
            ViewData["user"] = u;

            return Page();
        }

        public async Task<IActionResult> OnPostEditProfile()
        {
            var userJson = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Auth/Login");
            }
            var user = JsonConvert.DeserializeObject<User>(userJson);
            var jsonParam = new
            {
                name = User.Name,
                phone = User.Phone,
                dob = User.Dob
            };
            string jsonContent = JsonConvert.SerializeObject(jsonParam);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string url = @"https://localhost:7145/edit-profile/" + user.UserId;
            HttpResponseMessage result = await httpClient.PostAsync(url, content);
            string jsonStr = await result.Content.ReadAsStringAsync();

            var userInfo = JsonConvert.DeserializeObject<User>(jsonStr);
            ViewData["SuccessMessage"] = "Update successful!";
            ViewData["user"] = userInfo;
            return Page();
        }

        public async Task<IActionResult> OnPostChangePassword()
        {
            var userJson = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Auth/Login");
            }
            else
            {
                var user = JsonConvert.DeserializeObject<User>(userJson);
                ViewData["user"] = user;

                var jsonParam = new
                {
                    currentPassword = CurrentPassword,
                    newPassword = NewPassword,
                    rePassword = ConfirmPassword
                };
                string jsonContent = JsonConvert.SerializeObject(jsonParam);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                string url = @"https://localhost:7145/change-password/" + user.UserId;
                HttpResponseMessage result = await httpClient.PostAsync(url, content);
                if (result.IsSuccessStatusCode)
                {
                    ViewData["SuccessMessage"] = "Password changed successfully!";
                } else
                {
                    Message = await result.Content.ReadAsStringAsync();
                }
                return Page();
            }

        }

    }
}
