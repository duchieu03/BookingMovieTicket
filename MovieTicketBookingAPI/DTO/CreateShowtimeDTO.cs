namespace MovieTicketBookingAPI.DTO
{
    public class CreateShowtimeDTO
    {
        public int MovieId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Price { get; set; }
    }
}
