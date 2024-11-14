namespace MovieTicketBookingAPI.DTO
{
    public class TransactionDTO
    {
        public string TransactionRef { get; set; }
        public string Seats { get; set; }
        public int UserId { get; set; }
        public int ShowtimeId { get; set; }
        public DateTime BookDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string SelectedSeats { get; set; }
    }
}
