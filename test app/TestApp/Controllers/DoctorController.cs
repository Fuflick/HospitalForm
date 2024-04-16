using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public async Task<IActionResult> Index()
        {
            using MyDbContext dbContext = new MyDbContext();
            var doctors = await dbContext.Doctor.ToListAsync();
            return View(doctors);
        }

        // GET: Doctor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Specializations")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                using MyDbContext dbContext = new MyDbContext();
                dbContext.Add(doctor);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            using MyDbContext dbContext = new MyDbContext();
            var doctor = await dbContext.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            dbContext.Doctor.Remove(doctor);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}