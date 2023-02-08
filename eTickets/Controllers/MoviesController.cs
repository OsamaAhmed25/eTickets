using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task <IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync(x=>x.Cinema);
            return View(allMovies);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(x => x.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {

                // I Use This (StringComparison.OrdinalIgnoreCase) to avoid case sensitve
                var filterResult = allMovies.Where(x=>x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||x.Description.Contains(searchString)).ToList();
                return View("Index",filterResult);
            }
            else
            {
                
                return View("Index",allMovies);
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            return View(movieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetMoviesDropdownValues();
            ViewBag.CinemaId = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.ActorIds = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
            ViewBag.ProducerId = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetMoviesDropdownValues();
                ViewBag.CinemaId = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.ActorIds = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
                ViewBag.ProducerId = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                return View(movie);

            }

            await _service.AddNewMovieAsync(movie);
                return RedirectToAction(nameof(Index));
           
        }
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);

            if (movieDetails == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };
            var movieDropdownsData = await _service.GetMoviesDropdownValues();
            ViewBag.CinemaId = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.ActorIds = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
            ViewBag.ProducerId = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id , NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetMoviesDropdownValues();
                ViewBag.CinemaId = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.ActorIds = new SelectList(movieDropdownsData.Actors, "Id", "FullName");
                ViewBag.ProducerId = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                return View(movie);

            }

            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));

        }

    }
}
