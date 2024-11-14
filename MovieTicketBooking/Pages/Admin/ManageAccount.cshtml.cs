using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTicketBooking.Models;
using Newtonsoft.Json;
using System.Text;

namespace MovieTicketBooking.Pages.Admin
{
    public class ManageAccountModel : PageModel
    {
        private HttpClient httpClient;
        public ManageAccountModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public async Task<IActionResult> OnGet([FromQuery] int page = 1)
        {
            CurrentPage = page;
            var userJson = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Auth/Login");
            }
            var user = JsonConvert.DeserializeObject<User>(userJson);
            if (user == null || user.Role != 3)
            {
                return RedirectToPage("/Index");
            }
            string url = @"https://localhost:7145/api/Admin/GetAllUsersPage?page="+page;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(jsonStr);

            url = @"https://localhost:7145/api/Admin/GetAllUsersPageCount";
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            TotalPages = JsonConvert.DeserializeObject<int>(jsonStr);
            ViewData["users"] = users;
            ViewData["message"] = TempData["message"]?.ToString() ?? string.Empty;
            return Page();
        }

        public async Task<IActionResult> OnPostEditAccountAsync(User user)
        {
            user.Password = "";
            string jsonContent = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string url = @"https://localhost:7145/api/Admin/UpdateUser/"+user.UserId;
            HttpResponseMessage result = await httpClient.PostAsync(url, content);
            string jsonStr = await result.Content.ReadAsStringAsync();
            TempData["message"] = "Update User Successful";
            return RedirectToPage();
        }
    }
}
