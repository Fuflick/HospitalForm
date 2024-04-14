using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace test_app.Controllers
{
    public class ProcedureController : Controller
    {
        [HttpGet("Procedures")]
        public IActionResult Index()
        {
            var dbContext = new MyDbContext();
            var procedures= dbContext.Procedure.ToList();
           
            return View(procedures);
        }
    }
}