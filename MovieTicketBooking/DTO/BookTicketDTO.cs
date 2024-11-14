namespace MovieTicketBooking.DTO
{
    public class BookTicketDTO
    {
        public int UserId { get; set; }
        public int ShowtimeId { get; set; }
        public List<int> selectedSeats { get; set; }
        public string TransactionRef { get; set; }
    }
}
