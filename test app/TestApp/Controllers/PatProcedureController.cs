using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class PatProcedureController : Controller
    {
        // GET:  PatDiagnose
        public async Task<IActionResult> Index()
        {
            using MyDbContext dbContext = new MyDbContext();
            var patProcedure = await dbContext.PatProcedure.ToListAsync();
            return View(patProcedure);
        }

        // GET: PatDiagnose/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST:  PatDiagnose/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatId,ProcId")] PatProcedure patProcedure)
        {
            using MyDbContext dbContext = new MyDbContext();
            if (ModelState.IsValid)
            {
                dbContext.Add(patProcedure);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patProcedure);
        }

        // POST: DocDiagnose/Delete/7
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int patId, int procId)
        {
            using MyDbContext dbContext = new MyDbContext();
            var patProcedure = await dbContext.PatProcedure.FirstOrDefaultAsync(d => d.PatId == patId && d.Procid== procId);
            if (patProcedure == null)
            {
                return NotFound();
            }

            dbContext.PatProcedure.Remove(patProcedure);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}