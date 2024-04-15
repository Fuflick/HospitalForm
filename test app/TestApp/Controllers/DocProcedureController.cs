using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test_app.Models;

namespace test_app.Controllers
{
    public class DocProcedureController : Controller
    {
        // GET: DocProcedure/Create
        public IActionResult Index()
        {
            return View();
        }

        // POST: DocProcedure/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("DocId, ProcId")] DocProcedure docProcedure)
        {
            using MyDbContext dbContext = new MyDbContext();
            if (ModelState.IsValid)
            {
                dbContext.DocProcedure.Add(docProcedure);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(docProcedure);
        }
        
    }
}