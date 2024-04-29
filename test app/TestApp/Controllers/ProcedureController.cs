using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using test_app.Models;

namespace test_app.Controllers
{
    public class ProcedureController : Controller
    {
        private readonly MyDbContext _context;

        public ProcedureController()
        {
            _context = new MyDbContext();
        }

        // GET: Procedure
        public async Task<IActionResult> Index()
        {
            return View(await _context.Procedure.ToListAsync());
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
                _context.Add(procedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(procedure);
        }

        // GET: Procedure/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedure = await _context.Procedure.FindAsync(id);
            if (procedure == null)
            {
                return NotFound();
            }
            return View(procedure);
        }

        // POST: Procedure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Body,Cost,Date")] Procedure procedure)
        {
            if (id != procedure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedureExists(procedure.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(procedure);
        }

        // GET: Procedure/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedure = await _context.Procedure
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedure == null)
            {
                return NotFound();
            }

            return View(procedure);
        }

        // POST: Procedure/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procedure = await _context.Procedure.FindAsync(id);
            _context.Procedure.Remove(procedure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedureExists(int id)
        {
            return _context.Procedure.Any(e => e.Id == id);
        }
    }
}
