using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class DocProcedureController : Controller
    {
        // GET: DocProcedure
        public async Task<IActionResult> Index()
        {
            using MyDbContext dbContext = new MyDbContext();
            var docProcedures = await dbContext.DocProcedure.ToListAsync();
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
            using MyDbContext dbContext = new MyDbContext();
            if (ModelState.IsValid)
            {
                dbContext.Add(docProcedure);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docProcedure);
        }

        // POST: DocProcedure/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int docId, int procId)
        {
            using MyDbContext dbContext = new MyDbContext();
            var docProcedure = await dbContext.DocProcedure.FindAsync(docId, procId);
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