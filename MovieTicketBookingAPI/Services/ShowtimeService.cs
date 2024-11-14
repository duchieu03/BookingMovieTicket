using Microsoft.EntityFrameworkCore;
using MovieTicketBookingAPI.DTO;
using MovieTicketBookingAPI.Models;

namespace MovieTicketBookingAPI.Services
{
    public class ShowtimeService
    {
        private readonly MovieBookingContext _context;

        public ShowtimeService(MovieBookingContext context) => _context = context;

        public Dictionary<string, List<ShowtimeDTO>> GetShowtimesOfMovie(int movieId)
        {
            Dictionary<string, List<ShowtimeDTO>> mapShowtimeToDate = new Dictionary<string, List<ShowtimeDTO>>();
            List<Showtime> showtimes = _context.Showtimes.Where(st => st.MovieId == movieId)
            .Include(st => st.Room)
            .ToList();
            foreach (Showtime showtime in showtimes)
            {
                ShowtimeDTO showtimeDTO = new ShowtimeDTO();
                showtimeDTO.ShowtimeId = showtime.ShowtimeId;
                showtimeDTO.Price = showtime.Price;
                showtimeDTO.Room = showtime.Room.Name;
                showtimeDTO.Date = showtime.StartTime.HasValue
                    ? showtime.StartTime.Value.ToString("yyyy/MM/dd")
                    : "No Date Available";
                showtimeDTO.SlotTime = showtime.StartTime.HasValue
                    ? showtime.StartTime.Value.ToString("hh:mm:ss")
                    : "No Date Available";
                int? capacity = showtime.Room.Capacity;
                int bookedSeat = _context.Bookings.Where(t => t.Showtime.ShowtimeId == showtime.ShowtimeId).Count();

                if (bookedSeat < capacity)
                {
                    showtimeDTO.isFull = false;
                }
                else
                {
                    showtimeDTO.isFull = true;
                }
                AddShowtimeToMap(mapShowtimeToDate, showtimeDTO.Date, showtimeDTO);
            }
            return mapShowtimeToDate;
        }

        public decimal? GetPriceByShowtimeId(int showtimeId)
        {
            return _context.Showtimes.Where(st => st.ShowtimeId == showtimeId).Select(st => st.Price).FirstOrDefault();
        }

        public static void AddShowtimeToMap(Dictionary<string, List<ShowtimeDTO>> map, string key, ShowtimeDTO showtime)
        {
            if (!map.ContainsKey(key))
            {
                map[key] = new List<ShowtimeDTO>();
            }
            map[key].Add(showtime);
        }

    }
}
