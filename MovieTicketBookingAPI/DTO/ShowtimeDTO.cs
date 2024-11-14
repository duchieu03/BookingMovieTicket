namespace MovieTicketBookingAPI.DTO
{
    public class ShowtimeDTO
    {
        public int ShowtimeId { get; set; }
        public string Date { get; set; }
        public string Room {  get; set; }
        public decimal? Price { get; set; }
        public bool isFull { get; set; }
        public string SlotTime { get; set; }
    }
}
