using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class PatDiagnoseController : Controller
    {
        private readonly MyDbContext _context;

        public PatDiagnoseController()
        {
            _context = new MyDbContext();
        }

        // GET: DocProcedure
        public async Task<IActionResult> Index()
        {
            return View(await _context.PatDiagnose.ToListAsync());
        }

        // GET: DocProcedure/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocProcedure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatId,DiagId")] PatDiagnose patDiagnose)
        {
            if (ModelState.IsValid)
            {
                _context.PatDiagnose.Add(patDiagnose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patDiagnose);
        }

        // GET: PatDiagnise/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docProcedure = await _context.PatDiagnose.FindAsync(id);
            if (docProcedure == null)
            {
                return NotFound();
            }
            return View(docProcedure);
        }

        // POST: DocProcedure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatId,DiagId")] PatDiagnose patDiagnose)
        {
            if (id != patDiagnose.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patDiagnose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatDiagnoseExist(patDiagnose.Id))
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
            return View(patDiagnose);
        }

        // GET: PatDiagnose/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patDiagnose = await _context.PatDiagnose
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patDiagnose == null)
            {
                return NotFound();
            }

            return View(patDiagnose);
        }

        // POST: DocProcedure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patDiagnose = await _context.PatDiagnose.FindAsync(id);
            if (patDiagnose == null)
            {
                return NotFound();
            }

            // Проверка на наличие связей с Doctor и Procedure
            var hasDoctorLinks = _context.Patient.Any(d => d.Id == patDiagnose.PatId);
            var hasProcedureLinks = _context.Diagnose.Any(p => p.Id == patDiagnose.DiagId);
            if (hasDoctorLinks || hasProcedureLinks)
            {
                TempData["ErrorMessage"] = "Нельзя удалить связь, так как она связана с одним или несколькими врачами или процедурами.";
                return RedirectToAction(nameof(Index));
            }

            _context.PatDiagnose.Remove(patDiagnose);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private bool PatDiagnoseExist(int id)
        {
            return _context.PatDiagnose.Any(e => e.Id == id);
        }
    }
}
