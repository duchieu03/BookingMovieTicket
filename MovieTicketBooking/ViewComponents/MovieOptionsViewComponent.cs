using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Services;
using System;

public class MovieOptionsViewComponent : ViewComponent
{
    private readonly MovieService _movieService;

    public MovieOptionsViewComponent(MovieService movieService)
    {
        _movieService = movieService;
    }

    public IViewComponentResult Invoke()
    {
        var movies = _movieService.GetMovies();
        return View(movies);
    }
}
