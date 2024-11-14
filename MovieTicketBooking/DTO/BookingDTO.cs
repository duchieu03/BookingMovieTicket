namespace MovieTicketBooking.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime BookDate { get; set; }
        public DateTime ShowTime { get; set; }
        public int Status { get; set; }
        public string Seat { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
