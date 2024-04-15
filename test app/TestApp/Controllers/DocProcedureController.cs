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
        private readonly MyDbContext _dbContext;
        
        public DocProcedureController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocProcedure docProcedure)
        {
            if (ModelState.IsValid)
            {
                var docProceduremodel = new DocProcedure()
                {
                    DocId = 1,
                    ProcId = 1,
                };
                _dbContext.DocProcedure.Add(docProceduremodel);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Doctor");
            }
            return View(docProcedure);
        }
    }
}