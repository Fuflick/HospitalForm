using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class DocDiagnoseController : Controller
    {
        // GET: DocProcedure
        public async Task<IActionResult> Index()
        {
            using MyDbContext dbContext = new MyDbContext();
            var docDiagnose = await dbContext.DocDiagnose.ToListAsync();
            return View(docDiagnose);
        }

        // GET: DocDiagnose/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DocDiagnose/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocId,DiagId")] DocDiagnose docDiagnose)
        {
            using MyDbContext dbContext = new MyDbContext();
            if (ModelState.IsValid)
            {
                dbContext.Add(docDiagnose);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docDiagnose);
        }

        // POST: DocDiagnose/Delete/6
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int docId, int diagId)
        {
            using MyDbContext dbContext = new MyDbContext();
            var docDiagnose = await dbContext.DocDiagnose.FirstOrDefaultAsync(d => d.DocId == docId && d.DiagId == diagId);
            if (docDiagnose == null)
            {
                return NotFound();
            }

            dbContext.DocDiagnose.Remove(docDiagnose);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}