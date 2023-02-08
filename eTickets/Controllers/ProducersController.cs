using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        public ProducersController(IProducerService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }


        public IActionResult Create(int id)
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(producer);
                return RedirectToAction("Index");

            }
            return View();

        }

        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails != null)
            {
                return View(producerDetails);
            }
            else
            {
                return View("NotFound");
            }


        }
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails != null)
            {
                return View(producerDetails);
            }
            else
            {
                return View("NotFound");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);

            }
            await _service.UpdateAsync(id, producer);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails != null)
            {
                return View(producerDetails);
            }
            else
            {
                return View("NotFound");
            }

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails != null)
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
