using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;
using testapp.Migrations;

namespace test_app.Controllers
{
    public class DoctorController : Controller
    {
        private readonly MyDbContext _dbContext;

        public DoctorController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet("Doctors")]
        public IActionResult Index()
        {
            var doctors = new List<Doctor>
            {
                new Doctor() { Name = "bob", Specializations = [Specialization.Lor, Specialization.Surgeon] }
            };
        
            return View(doctors);
        }
    }
}