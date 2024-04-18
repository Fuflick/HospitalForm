using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class PatDiagnoseController : Controller
    {
        // GET:  PatDiagnose
        public async Task<IActionResult> Index()
        {
            using MyDbContext dbContext = new MyDbContext();
            var patDiagnose = await dbContext.PatDiagnose.ToListAsync();
            return View(patDiagnose);
        }

        // GET: PatDiagnose/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST:  PatDiagnose/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatId,DiagId")] PatDiagnose patDiagnose)
        {
            using MyDbContext dbContext = new MyDbContext();
            if (ModelState.IsValid)
            {
                dbContext.Add(patDiagnose);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patDiagnose);
        }

        // POST: DocDiagnose/Delete/7
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int patId, int diagId)
        {
            using MyDbContext dbContext = new MyDbContext();
            var patDiagnose = await dbContext.PatDiagnose.FirstOrDefaultAsync(d => d.PatId == patId && d.DiagId == diagId);
            if (patDiagnose == null)
            {
                return NotFound();
            }

            dbContext.PatDiagnose.Remove(patDiagnose);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}