using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using MovieTicketBooking.DTO;
using MovieTicketBooking.Hubs;
using MovieTicketBooking.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace MovieTicketBooking.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private HttpClient httpClient;
        public LoginModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        [BindProperty]
        public User user { get; set; }
        public string Message { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }

        public bool ShowRegisterForm;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogin()
        {
            LoginDTO loginDTO = new LoginDTO
            {
                email = user.Email,
                password = user.Password
            };

            string jsonContent = JsonConvert.SerializeObject(loginDTO);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            string url = @"https://localhost:7145/login";
            HttpResponseMessage result = await httpClient.PostAsync(url, content);
            string jsonStr = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                var userInfo = JsonConvert.DeserializeObject<User>(jsonStr);

                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(userInfo));
                return userInfo.Role == 3 ? RedirectToPage("/Admin/Dashboard") :
                       userInfo.Role == 2 ? RedirectToPage("/Staff/StaffMain") :
                       RedirectToPage("/Index");
            }
            else
            {
                Message = jsonStr;
                return Page();
            }
        }


        public async Task<IActionResult> OnPostRegisterAsync()
        {
            RegisterDTO register = new RegisterDTO
            {
                email = user.Email,
                password = user.Password,
                confirmPassword = ConfirmPassword,
                name = user.Name,
                birthDay = user.Dob
            };

            string jsonContent = JsonConvert.SerializeObject(register);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string url = @"https://localhost:7145/register";
            HttpResponseMessage result = await httpClient.PostAsync(url, content);
            string jsonStr = await result.Content.ReadAsStringAsync();
            if (jsonStr.Equals("Created user"))
            {
                var verificationLink = Url.Page(
                "/Auth/VerifyAccount",
                pageHandler: null,
                values: new { email = user.Email },
                protocol: Request.Scheme
            );
                var emailBody = $@"
        <h2>Welcome to our system</h2>
        <p>Please click the link below to verify your account:</p>
        <a href=""{verificationLink}"">Click here to verify your account!</a>";
                SendMailSMTP.SendMail(user.Email, "Verify new user!", emailBody);
            }
            else
            {
                ShowRegisterForm = true;
                Message = jsonStr;
            }

            return Page();
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }


        public async Task<IActionResult> OnPostForgotPassword()
        {
            string url = @"https://localhost:7145/forgot?email=" + user.Email;
            var forgotPasswordData = new { email = user.Email };
            StringContent content = new StringContent(JsonConvert.SerializeObject(forgotPasswordData), Encoding.UTF8, "application/json");

            HttpResponseMessage result = await httpClient.PostAsync(url, content);
            Message = await result.Content.ReadAsStringAsync(); return Page();
        }

    }
}
