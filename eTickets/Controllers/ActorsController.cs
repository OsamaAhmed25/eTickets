using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Collections.Specialized.BitVector32;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorService _service;
        public ActorsController(IActorService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data =await _service.GetAllAsync();
            return View(data);
        }
        public IActionResult Create(int id)
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            if (ModelState.IsValid)
            {
               await _service.AddAsync(actor);
                return RedirectToAction("Index");

            }
            return View();

        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails!=null)
            {
                return View(actorDetails);
            }
            else
            {
                return View("NotFound");
            }
           

        }
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails != null)
            {
                return View(actorDetails);
            }
            else
            {
                return View("NotFound");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);

            }
            await _service.UpdateAsync(id,actor);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails != null)
            {
                return View(actorDetails);
            }
            else
            {
                return View("NotFound");
            }

        }
        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails != null)
            {
                await _service.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
              
            }

           

        }
    }
}
