using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemaService _service;

        public CinemasController(ICinemaService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCinemas =await _service.GetAllAsync();
            return View(allCinemas);
        }
        public IActionResult Create(int id)
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Logo,Description")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(cinema);
                return RedirectToAction("Index");

            }
            return View();

        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails != null)
            {
                return View(cinemaDetails);
            }
            else
            {
                return View("NotFound");
            }


        }
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails != null)
            {
                return View(cinemaDetails);
            }
            else
            {
                return View("NotFound");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);

            }
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails != null)
            {
                return View(cinemaDetails);
            }
            else
            {
                return View("NotFound");
            }

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails != null)
            {
                await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("NotFound");

            }



        }
    }
}
