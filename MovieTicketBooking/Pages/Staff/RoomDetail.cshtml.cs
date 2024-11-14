using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.DTO;
using MovieTicketBooking.Models;
using Newtonsoft.Json;

namespace MovieTicketBooking.Pages.Staff
{
    public class RoomDetailModel : PageModel
    {
        private HttpClient httpClient;

        public RoomDetailModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Room? Room { get; set; }
        public List<SeatDTO> Seats = new List<SeatDTO>();

        public async Task<IActionResult> OnGet(int roomId)
        {
            var userJson = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userJson))
            {
                return RedirectToPage("/Auth/Login");
            }
            var user = JsonConvert.DeserializeObject<User>(userJson);
            if (user == null || user.Role != 2)
            {
                return RedirectToPage("/Index");
            }

            string url = @"https://localhost:7145/api/Staff/GetRoom/" + roomId;
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            
            
            Seats = JsonConvert.DeserializeObject<List<SeatDTO>>(jsonStr);
            url = @"https://localhost:7145/api/Staff/GetRoomDetail/" + roomId;
            result = await httpClient.GetAsync(url);
            jsonStr = await result.Content.ReadAsStringAsync();
            Room r = JsonConvert.DeserializeObject<Room>(jsonStr);
            result = await httpClient.GetAsync(url);
           
            ViewData["room"] = r;
            ViewData["user"] = user.Name;
            return Page();
        }
    }
}
