using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class DocProcedureController : Controller
    {
        private readonly MyDbContext _context;

        public DocProcedureController()
        {
            _context = new MyDbContext();
        }

        // GET: DocProcedure
        public async Task<IActionResult> Index()
        {
            return View(await _context.DocProcedure.ToListAsync());
        }

        // GET: DocProcedure/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocProcedure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DocId,ProcId")] DocProcedure docProcedure)
        {
            if (ModelState.IsValid)
            {
                _context.DocProcedure.Add(docProcedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docProcedure);
        }

        // GET: DocProcedure/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docProcedure = await _context.DocProcedure.FindAsync(id);
            if (docProcedure == null)
            {
                return NotFound();
            }
            return View(docProcedure);
        }

        // POST: DocProcedure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DocId,ProcId")] DocProcedure docProcedure)
        {
            if (id != docProcedure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docProcedure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocProcedureExists(docProcedure.Id))
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
            return View(docProcedure);
        }

        // GET: DocProcedure/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docProcedure = await _context.DocProcedure
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
            var docProcedure = await _context.DocProcedure.FindAsync(id);
            if (docProcedure == null)
            {
                return NotFound();
            }

            // Проверка на наличие связей с Doctor и Procedure
            var hasDoctorLinks = _context.Doctor.Any(d => d.Id == docProcedure.DocId);
            var hasProcedureLinks = _context.Procedure.Any(p => p.Id == docProcedure.ProcId);
            if (hasDoctorLinks || hasProcedureLinks)
            {
                TempData["ErrorMessage"] = "Нельзя удалить процедуру, так как она связана с одним или несколькими врачами или процедурами.";
                return RedirectToAction(nameof(Index));
            }

            _context.DocProcedure.Remove(docProcedure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        


        private bool DocProcedureExists(int id)
        {
            return _context.DocProcedure.Any(e => e.Id == id);
        }
    }
}
