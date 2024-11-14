using System;
using System.Collections.Generic;

namespace MovieTicketBookingAPI.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public string TransactionRef { get; set; } = null!;
        public int UserId { get; set; }
        public int ShowtimeId { get; set; }
        public DateTime BookDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string SelectedSeats { get; set; } = null!;
    }
}
