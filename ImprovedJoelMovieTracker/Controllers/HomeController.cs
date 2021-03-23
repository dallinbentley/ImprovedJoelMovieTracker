using ImprovedJoelMovieTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ImprovedJoelMovieTracker.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext context { get; set; }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MovieContext con)
        {
            _logger = logger;
            context = con;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MovieList()
        {
            return View(new MovieListViewModel
            {
                movies = context.Movies
            });
        }

        [HttpPost]
        public IActionResult GoToEdit(MovieListViewModel model)
        {
            Movie editedMovie = new Movie();

            editedMovie = context.Movies.Find(model.MovieId);

            return View("EditMovie", editedMovie);
        }

        [HttpPost]
        public IActionResult EditMovie(Movie editedMovie)
        {
           
            context.Movies.Where(m => m.MovieId == editedMovie.MovieId).FirstOrDefault().Category = editedMovie.Category;
            context.Movies.Where(m => m.MovieId == editedMovie.MovieId).FirstOrDefault().Title = editedMovie.Title;
            context.Movies.Where(m => m.MovieId == editedMovie.MovieId).FirstOrDefault().Director = editedMovie.Director;
            context.Movies.Where(m => m.MovieId == editedMovie.MovieId).FirstOrDefault().Year = editedMovie.Year;
            context.Movies.Where(m => m.MovieId == editedMovie.MovieId).FirstOrDefault().Rating = editedMovie.Rating;
            context.Movies.Where(m => m.MovieId == editedMovie.MovieId).FirstOrDefault().Edited = editedMovie.Edited;
            context.Movies.Where(m => m.MovieId == editedMovie.MovieId).FirstOrDefault().LentTo = editedMovie.LentTo;
            context.Movies.Where(m => m.MovieId == editedMovie.MovieId).FirstOrDefault().Notes = editedMovie.Notes;

            context.SaveChanges();

            return View("MovieList", new MovieListViewModel
            {
                movies = context.Movies
            });
        }

        [HttpPost]
        public IActionResult DeleteMovie(MovieListViewModel model)
        {
            context.Movies.Remove(context.Movies.Find(model.MovieId));

            context.SaveChanges();

            return View("MovieList", new MovieListViewModel 
            {
                movies = context.Movies
            });
        }

        public IActionResult Podcast()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SubmitMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitMovie(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                context.Movies.Add(newMovie);

                context.SaveChanges();
            }
            return View("Confirmation");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
