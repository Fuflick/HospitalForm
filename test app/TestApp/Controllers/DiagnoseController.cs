using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class DiagnoseController : Controller
    {
        private static MyDbContext dbContext = new MyDbContext();
        // GET: Diagnose
        public async Task<IActionResult> Index()
        {
            var diagnoses = await dbContext.Diagnose.ToListAsync();
            return View(diagnoses);
        }

        // GET: Diagnose/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Diagnose/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Body,Date")] Diagnose diagnose)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(diagnose);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnose);
        }

        // POST: Diagnose/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var diagnose = await dbContext.Diagnose.FindAsync(id);
            if (diagnose == null)
            {
                return NotFound();
            }

            dbContext.Diagnose.Remove(diagnose);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}