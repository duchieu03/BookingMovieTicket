using Microsoft.EntityFrameworkCore;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Models;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;

namespace MovieTicketBookingAPI.Services
{
    public class SeatService
    {
        private readonly MovieBookingContext _context;
        private HttpClient _httpClient;
        public SeatService(MovieBookingContext context, HttpClient client)
        {
            _context = context;
            _httpClient = client;
        }

        public Dictionary<string, List<SeatDTO>> GetSeats(int showtimeId)
        {
            Dictionary<string, List<SeatDTO>> seats = new Dictionary<string, List<SeatDTO>>();
            Showtime showtime = _context.Showtimes.Where(t => t.ShowtimeId == showtimeId)
                .Include(t => t.Room)
                .Include(t => t.Bookings)
                .FirstOrDefault();

            List<Seat> seatList = _context.Seats.Where(t => t.RoomId == showtime.RoomId).ToList();
            List<Seat> bookedSeats = new List<Seat>();
            foreach (var booking in showtime.Bookings)
            {
                List<BookingDetail> bookingDetails = _context.BookingDetails.Where(t => t.BookingId == booking.BookingId)
                     .Include(t => t.Seat).ToList();
                foreach (var bookingDetail in bookingDetails)
                {
                    if (bookingDetail != null && bookingDetail.Seat != null)
                    {
                        bookedSeats.Add(bookingDetail.Seat);
                    }
                }
            }

            foreach (var seat in seatList)
            {
                SeatDTO seatDTO = new SeatDTO
                {
                    SeatId = seat.SeatId,
                    Name = seat.SeatNumber,
                    IsFree = !bookedSeats.Any(b => b.SeatId == seat.SeatId)
                };

                string row = seat.SeatNumber.Substring(0, 1);
                // Group seats by row in the dictionary
                if (!seats.ContainsKey(row))
                {
                    seats[row] = new List<SeatDTO>();
                }
                seats[row].Add(seatDTO);
            }

            return seats;
        }

        public TransactionDTO bookTicket(BookTicketDTO request)
        {
            Showtime showtime = _context.Showtimes.Where(t => t.ShowtimeId == request.ShowtimeId).First();
            User user = _context.Users.FirstOrDefault(t => t.UserId == request.UserId);

            //Save transaction
            var Transaction = new TransactionDTO();
            Transaction.TransactionRef = request.TransactionRef;
            Transaction.UserId = request.UserId;
            Transaction.ShowtimeId = request.ShowtimeId;
            Transaction.BookDate = DateTime.Now;
            Transaction.TotalPrice = showtime.Price * request.selectedSeats.Count;
            Transaction.SelectedSeats = String.Join(",", request.selectedSeats);

            Transaction transaction = new Transaction();
            transaction.TransactionRef = request.TransactionRef;
            transaction.UserId = request.UserId;
            transaction.ShowtimeId = request.ShowtimeId;
            transaction.BookDate = Transaction.BookDate;
            transaction.TotalPrice = showtime.Price * request.selectedSeats.Count;
            transaction.SelectedSeats = Transaction.SelectedSeats;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            return Transaction;
        }

        public int handleBookingTicket(string TransactionRef)
        {
            var Transaction = _context.Transactions.Where(t => t.TransactionRef == TransactionRef).FirstOrDefault();
            User user = _context.Users.FirstOrDefault(t => t.UserId == Transaction.UserId);

            Booking booking = new Booking();
            booking.UserId = Transaction.UserId;
            booking.ShowtimeId = Transaction.ShowtimeId;
            booking.BookDate = Transaction.BookDate;
            booking.TotalPrice = Transaction.TotalPrice;
            booking.Status = (int)BookingStatus.Paid;
            booking.PaymentMethod = (int)PaymentMethod.Online;

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            var seatIds = Transaction.SelectedSeats.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var selectedSeat in seatIds)
            {
                BookingDetail detail = new BookingDetail();
                detail.SeatId = int.Parse(selectedSeat);
                detail.BookingId = booking.BookingId;

                _context.BookingDetails.Add(detail);
            }
            _context.SaveChanges();
            SendMailAferBooking(user.Email, Transaction);
            return Transaction.ShowtimeId;
        }

        public void test()
        {
            TestSendMailAferBooking("duchieu20092003@gmail.com", "test");
        }

        private string buildEmailBooking(Transaction transaction)
        {
            Showtime showtime = _context.Showtimes.Include(t=> t.Movie).FirstOrDefault(t=> t.ShowtimeId == transaction.ShowtimeId);
            var seatIds = transaction.SelectedSeats.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string roomName = "";
            string seats = "";
            foreach (var seat in seatIds)
            {
                Seat seat1 = _context.Seats.Include(t => t.Room).Where(t => t.SeatId == int.Parse(seat)).FirstOrDefault();
                roomName = seat1.Room.Name;
                seats += seat1.SeatNumber + " ";
            }
            string res = "<h3>Bạn đã đặt vé phim : " + showtime.Movie.Title + "</h3>" + "<p>Chỗ ngồi: "+ seats +"</p>" + "<p>Phòng : "+ roomName +"</p>" + "<p>Thời gian bắt đầu: "+ showtime.StartTime +"</p>";
            return res;
        }

        public int handleFailBookingTicket(string TransactionRef)
        {
            var Transaction = _context.Transactions.Where(t => t.TransactionRef == TransactionRef).FirstOrDefault();
            return Transaction.ShowtimeId;
        }


        private async void SendMailAferBooking(string email, Transaction transaction)
        {
            string body = buildEmailBooking(transaction);
            MemoryStream attachment = await GenerateQrCode(body);
            SendMailSMTP.SendMailWithAttachment(email, "Đặt vé xem phim", body, attachment);
        }

        private async void TestSendMailAferBooking(string email,string body)
        {
            MemoryStream attachment = await GenerateQrCode(body);
            SendMailSMTP.SendMailWithAttachment(email, "Đặt vé xem phim", body, attachment);
        }

        private async Task<MemoryStream> GenerateQrCode(string payload)
        {
            try
            {
                var clientHttp = new HttpClient();
                clientHttp.Timeout = TimeSpan.FromMinutes(30);

                string accessToken = "cdVV0R7_PjAcCmXoTB0QQfgae5jfwQi4I0KQueVo0LxeOE4v__eUX6iMIPCBxAED";
                string apiUrl = $"https://api.qr-code-generator.com/v1/create?access-token={accessToken}";
                string jsonPayload = $@"
                {{
                    ""frame_name"": ""no-frame"",
                    ""qr_code_text"": ""{payload}"",
                    ""image_format"": ""JPG"",
                    ""qr_code_logo"": ""scan-me-square""
                }}";
                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage result = await clientHttp.PostAsync(apiUrl, content);
                // Create the JSON payload dynamically
                byte[] pngBytes = await result.Content.ReadAsByteArrayAsync();
                return new MemoryStream(pngBytes);
            }
            catch (TaskCanceledException ex)
            {
                Console.WriteLine($"Request timed out: {ex.Message}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message} == {ex.StackTrace}");
                return null;
            }
        }

    }
}
