using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;
using System.Threading.Tasks;

namespace test_app.Controllers
{
    public class DocProcedureController : Controller
    {
        
        // GET: DocProcedure
        public async Task<IActionResult> Index()
        {
            var dbConext = new MyDbContext();
            var docProcedures = await dbConext.DocProcedure.ToListAsync();
            return View(docProcedures);
        }

        // GET: DocProcedure/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocProcedure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocId,ProcId")] DocProcedure docProcedure)
        {
            var dbConext = new MyDbContext();
            if (ModelState.IsValid)
            {
                dbConext.Add(docProcedure);
                await dbConext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docProcedure);
        }

        // POST: DocProcedure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int docId, int procId)
        {
            var dbContext = new MyDbContext();
            var docProcedure = await dbContext.DocProcedure.FirstOrDefaultAsync(p => p.DocId == docId && p.ProcId == procId);
            if (docProcedure == null)
            {
                return NotFound();
            }

            dbContext.DocProcedure.Remove(docProcedure);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}