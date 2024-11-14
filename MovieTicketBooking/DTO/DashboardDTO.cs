namespace MovieTicketBooking.DTO
{
    public class DashboardDTO
    {
        public int NumberOfUsers { get; set; }
        public int NumberOfMovies { get; set; }
        public int RecentBookingsCount { get; set; }
        public decimal? TotalRevenueLast7Days { get; set; }
        public string GenresLabels { get; set; }
        public string GenresCounts { get; set; }
        public string MovieMonths { get; set; }
        public string MovieCountsPerMonth { get; set; }
        public string Top4MoviesLabels { get; set; }
        public string Top4MoviesCounts { get; set; }
        public string RevenueMonths { get; set; }
        public string RevenuePerMonth { get; set; }
        public string TopUsersLabels { get; set; }
        public string TopUsersCounts { get; set; }
    }
}
