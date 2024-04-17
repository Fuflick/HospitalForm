using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class ProcedureController : Controller
    {

        // GET: Procedure
        public async Task<IActionResult> Index()
        {
            using MyDbContext dbContext = new MyDbContext();
            var procedures = await dbContext.Procedure.ToListAsync();
            return View(procedures);
        }

        // GET: Procedure/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Procedure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Body,Cost,Date")] Procedure procedure)
        {
            if (ModelState.IsValid)
            {
                using MyDbContext dbContext = new MyDbContext();
                dbContext.Add(procedure);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procedure);
        }

        // POST: Procedure/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            using MyDbContext dbContext = new MyDbContext();
            var procedure = await dbContext.Procedure.FindAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }

            dbContext.Procedure.Remove(procedure);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}