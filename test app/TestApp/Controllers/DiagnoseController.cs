using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using test_app.Models;

namespace test_app.Controllers
{
    public class DiagnoseController : Controller
    {
        private readonly MyDbContext _context;

        public DiagnoseController()
        {
            _context = new MyDbContext();
        }

        // GET: Diagnose
        public async Task<IActionResult> Index()
        {
            return View(await _context.Diagnose.ToListAsync());
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
                _context.Add(diagnose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(diagnose);
        }

        // GET: Diagnose/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnose = await _context.Diagnose.FindAsync(id);
            if (diagnose == null)
            {
                return NotFound();
            }
            return View(diagnose);
        }

        // POST: Diagnose/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Body,Date")] Diagnose diagnose)
        {
            if (id != diagnose.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diagnose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnoseExists(diagnose.Id))
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
            return View(diagnose);
        }

        // GET: Diagnose/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnose = await _context.Diagnose
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnose == null)
            {
                return NotFound();
            }

            return View(diagnose);
        }

        // POST: Diagnose/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnose = await _context.Diagnose.FindAsync(id);
            _context.Diagnose.Remove(diagnose);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiagnoseExists(int id)
        {
            return _context.Diagnose.Any(e => e.Id == id);
        }
    }
}
