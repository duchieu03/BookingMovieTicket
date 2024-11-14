using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Common;
using MovieTicketBooking.Models;

namespace MovieTicketBooking.Services
{
    public class MovieService
    {
        private readonly MovieBookingContext _context;

        public MovieService(MovieBookingContext context)
        {
            _context = context;
        }

        public List<Movie> GetMovies()
            => _context.Movies.Include(m => m.Actors)
                            .Include(m => m.Genres)
                            .Include(m => m.Showtimes)
                            .Where(m => m.Status == (int)MovieStatus.NowShowing).ToList();
        public List<Movie> GetNLastestMovies(string keyword)
            => _context.Movies.Include(m => m.Actors)
                            .Include(m => m.Genres)
                            .Include(m => m.Showtimes)
                            .Where(m => m.Title.Contains(keyword) && (m.Status == (int)MovieStatus.NowShowing || m.Status == (int)MovieStatus.ComingSoon))
                            .OrderByDescending(m => m.ReleaseDate)
                            .ToList();

        public int GetResultCount(string keyword)
            => _context.Movies.Where(m => m.Title.Contains(keyword) && (m.Status == (int)MovieStatus.NowShowing || m.Status == (int)MovieStatus.ComingSoon)).Count();

        public Movie? GetMovie(int id) => _context.Movies.Where(m => m.MovieId == id).Include(m => m.Genres).Include(m => m.Actors).Include(m => m.Showtimes).First();

        public void AddMMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            var movieToUpdate = GetMovie(movie.MovieId);

            if (movieToUpdate == null)
            {
                throw new Exception("Movie not found");
            }

            _context.Entry(movieToUpdate).CurrentValues.SetValues(movie);
            _context.SaveChanges();
        }

        public void RemoveMovie(int id)
        {
            var movieToRemove = GetMovie(id);

            if (movieToRemove == null)
            {
                throw new Exception("Movie not found");
            }

            _context.Remove(movieToRemove);
            _context.SaveChanges();
        }
    }
}
