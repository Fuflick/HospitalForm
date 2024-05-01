using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class DocDiagnoseController : Controller
    {
        private readonly MyDbContext _context;

        public DocDiagnoseController()
        {
            _context = new MyDbContext();
        }

        // GET: DocProcedure
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocDiagnose.ToListAsync());
        }

        // GET: DocProcedure/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocProcedure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocId,DiagId")] DocDiagnose docDiagnose)
        {
            if (ModelState.IsValid)
            {
                _context.DocDiagnose.Add(docDiagnose);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docDiagnose);
        }

        // GET: DocProcedure/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docDiagnose = await _context.DocDiagnose.FindAsync(id);
            if (docDiagnose == null)
            {
                return NotFound();
            }
            return View(docDiagnose);
        }

        // POST: DocProcedure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DocId,DiagId")] DocDiagnose docDiagnose)
        {
            if (id != docDiagnose.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docDiagnose);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocProcedureExists(docDiagnose.Id))
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
            return View(docDiagnose);
        }

        // GET: DocProcedure/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docProcedure = await _context.DocDiagnose
                .FirstOrDefaultAsync(m => m.Id == id);
            if (docProcedure == null)
            {
                return NotFound();
            }

            return View(docProcedure);
        }

        // POST: DocProcedure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var docDiagnose = await _context.DocDiagnose.FindAsync(id);
            if (docDiagnose == null)
            {
                return NotFound();
            }

            // Проверка на наличие связей с Doctor и Procedure
            var hasDoctorLinks = _context.Doctor.Any(d => d.Id == docDiagnose.DocId);
            var hasProcedureLinks = _context.Diagnose.Any(p => p.Id == docDiagnose.DiagId);
            if (hasDoctorLinks || hasProcedureLinks)
            {
                TempData["ErrorMessage"] = "Нельзя удалить процедуру, так как она связана с одним или несколькими врачами или процедурами.";
                return RedirectToAction(nameof(Index));
            }

            _context.DocDiagnose.Remove(docDiagnose);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        


        private bool DocProcedureExists(int id)
        {
            return _context.DocDiagnose.Any(e => e.Id == id);
        }
    }
}
