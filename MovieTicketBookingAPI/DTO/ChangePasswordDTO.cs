namespace MovieTicketBookingAPI.DTO
{
    public class ChangePasswordDTO
    {
        public string currentPassword {  get; set; }
        public string newPassword { get; set; }
        public string rePassword { get; set; }
    }
}
