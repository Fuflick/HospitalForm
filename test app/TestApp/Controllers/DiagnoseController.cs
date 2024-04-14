using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace test_app.Controllers
{
    public class DiagnoseController : Controller
    {
        [HttpGet("Diagnoses")]
        public IActionResult Index()
        {
            using MyDbContext dbContext = new MyDbContext();
            var diagnoses = dbContext.Diagnose.ToList();
            return View(diagnoses);
        }
    }
}