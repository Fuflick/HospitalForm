using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class PatProcedureController : Controller
    {
        private readonly MyDbContext _context;

        public PatProcedureController()
        {
            _context = new MyDbContext();
        }

        // GET: DocProcedure
        public async Task<IActionResult> Index()
        {
            return View(await _context.PatProcedure.ToListAsync());
        }

        // GET: DocProcedure/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatProcedure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatId,ProcId")] PatProcedure patProcedure)
        {
            if (ModelState.IsValid)
            {
                _context.PatProcedure.Add(patProcedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patProcedure);
        }

        // GET: PatProcedure/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docProcedure = await _context.PatProcedure.FindAsync(id);
            if (docProcedure == null)
            {
                return NotFound();
            }
            return View(docProcedure);
        }

        // POST: DocProcedure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatId,ProcId")] PatProcedure patProcedure)
        {
            if (id != patProcedure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patProcedure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocProcedureExists(patProcedure.Id))
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
            return View(patProcedure);
        }

        // GET: DocProcedure/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patProcedure = await _context.PatProcedure
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patProcedure == null)
            {
                return NotFound();
            }

            return View(patProcedure);
        }

        // POST: DocProcedure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patProcedure = await _context.PatProcedure.FindAsync(id);
            if (patProcedure == null)
            {
                return NotFound();
            }

            // Проверка на наличие связей с Doctor и Procedure
            var hasDoctorLinks = _context.Patient.Any(d => d.Id == patProcedure.PatId);
            var hasProcedureLinks = _context.Procedure.Any(p => p.Id == patProcedure.Procid);
            if (hasDoctorLinks || hasProcedureLinks)
            {
                TempData["ErrorMessage"] = "Нельзя удалить связь, так как она связана с одним или несколькими пациентами или процедурами.";
                return RedirectToAction(nameof(Index));
            }

            _context.PatProcedure.Remove(patProcedure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        


        private bool DocProcedureExists(int id)
        {
            return _context.DocProcedure.Any(e => e.Id == id);
        }
    }
}
