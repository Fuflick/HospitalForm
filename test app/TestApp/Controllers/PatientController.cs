using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace test_app.Controllers
{
    public class PatientController : Controller
    {
        [HttpGet("Patients")]
        public IActionResult Index()
        {
            var dbContext = new MyDbContext();
            var patients = dbContext.Patient.ToList();
           
            return View(patients);
        }
        
    }
}