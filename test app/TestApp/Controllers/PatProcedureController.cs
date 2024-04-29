using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;
using System.Threading.Tasks;

namespace test_app.Controllers
{
    public class PatProcedureController : Controller
    {
        
        // GET: PatProcedure
        public async Task<IActionResult> Index()
        {
            var dbContext = new MyDbContext();
            var patProcedures = await dbContext.PatProcedure.ToListAsync();
            return View(patProcedures);
        }

        // GET: PatProcedure/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatProcedure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatId,Procid")] PatProcedure patProcedure)
        {
            var dbContext = new MyDbContext();
            if (ModelState.IsValid)
            {
                dbContext.Add(patProcedure);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patProcedure);
        }

        // POST: PatProcedure/Delete/5
        // POST: PatProcedure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int patId, int procId)
        {
            var dbContext = new MyDbContext();
            var patProcedure = await dbContext.PatProcedure.FirstOrDefaultAsync(p => p.PatId == patId && p.Procid == procId);
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