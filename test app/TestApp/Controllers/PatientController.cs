using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public async Task<IActionResult> Index()
        {
            using MyDbContext dbContext = new MyDbContext();
            var patients = await dbContext.Patient.ToListAsync();
            return View(patients);
        }

        // GET: Patient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                using MyDbContext dbContext = new MyDbContext();
                dbContext.Add(patient);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            using MyDbContext dbContext = new MyDbContext();
            var patient = await dbContext.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            dbContext.Patient.Remove(patient);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}