using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_app.Models;

namespace test_app.Controllers
{
    public class PatientController : Controller
    {
        private readonly MyDbContext _dbContext;

        public PatientController()
        {
            _dbContext = new MyDbContext();
        }

        // GET: Patient
        public async Task<IActionResult> Index()
        {
            var patients = await _dbContext.Patient.ToListAsync();
            return View(patients);
        }

        // GET: Patient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(patient);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _dbContext.Patient.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _dbContext.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _dbContext.Patient.Remove(patient);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // GET: Patient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _dbContext.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

// POST: Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(patient);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
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
            return View(patient);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DiscountConfirmed(int patientId)
        {
            // Найти все связанные процедуры для данного пациента
            var patProcedures = _dbContext.PatProcedure.Where(x => x.PatId == patientId).ToList();

            // Уменьшить цены на эти процедуры на заданное значение
            foreach (var patProcedure in patProcedures)
            {
                var procedure = await _dbContext.Procedure.FirstOrDefaultAsync(x => x.Id == patProcedure.Procid);
                if (procedure != null)
                {
                    procedure.Cost *= (decimal)0.9;
                    _dbContext.Update(procedure);
                }
            }

            // Сохранить изменения в базе данных
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); // Перенаправление на нужную страницу
        }

        public async Task<IActionResult> GiveDiscount(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _dbContext.Patient.FirstOrDefaultAsync(p => p.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }


        private bool PatientExists(int id)
        {
            return _dbContext.Patient.Any(p => p.Id == id);
        }
    }
}
