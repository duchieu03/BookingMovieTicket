namespace MovieTicketBooking.DTO
{
    public class RegisterDTO
    {
        public string email { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }
        public string name { get; set; }
        public DateTime? birthDay { get; set; }
    }
}
