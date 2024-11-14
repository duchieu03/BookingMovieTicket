using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.DTO;
using MovieTicketBooking.Hubs;
using MovieTicketBooking.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace MovieTicketBooking.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private HttpClient httpClient;
        public DashboardModel(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public int NumberOfUsers { get; set; }
        public int NumberOfMovies { get; set; }
        public int RecentBookingsCount { get; set; }
        public decimal? TotalRevenueLast7Days { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //var userJson = HttpContext.Session.GetString("user");
            //if (string.IsNullOrEmpty(userJson))
            //{
            //    return RedirectToPage("/Auth/Login");
            //}
            //var user = JsonConvert.DeserializeObject<User>(userJson);
            //if (user == null || user.Role != 3)
            //{
            //    return RedirectToPage("/Index");
            //}
            string url = @"https://localhost:7145/api/Admin/Dashboard";
            HttpResponseMessage result = await httpClient.GetAsync(url);
            string jsonStr = await result.Content.ReadAsStringAsync();
            DashboardDTO dashboard = JsonConvert.DeserializeObject<DashboardDTO>(jsonStr);
            ViewData["NumberOfUsers"] = dashboard.NumberOfUsers;
            ViewData["NumberOfMovies"] = dashboard.NumberOfMovies;
            ViewData["TotalRevenueLast7Days"] = dashboard.TotalRevenueLast7Days;
            ViewData["GenresLabels"] = dashboard.GenresLabels;
            ViewData["GenresCounts"] = dashboard.GenresCounts;
            ViewData["MovieMonths"] = dashboard.MovieMonths;
            ViewData["MovieCountsPerMonth"] = dashboard.MovieCountsPerMonth;
            ViewData["Top3MoviesLabels"] = dashboard.Top4MoviesLabels;
            ViewData["Top3MoviesCounts"] = dashboard.Top4MoviesCounts;
            ViewData["RevenueMonths"] = dashboard.RevenueMonths;
            ViewData["RevenuePerMonth"] = dashboard.RevenuePerMonth;
            ViewData["TopUsersLabels"] = dashboard.TopUsersLabels;
            ViewData["TopUsersCounts"] = dashboard.TopUsersCounts;
            setData(dashboard);
            return Page();
        }

        private void setData(DashboardDTO dashboard)
        {
            NumberOfUsers = dashboard.NumberOfUsers;
            NumberOfMovies = dashboard.NumberOfMovies;
            RecentBookingsCount = dashboard.RecentBookingsCount;
            TotalRevenueLast7Days = dashboard.TotalRevenueLast7Days;
        }
    }
}
