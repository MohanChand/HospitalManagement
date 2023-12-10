using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagementAPI.DBContext;
using HospitalManagementAPI.Model;

namespace HospitalManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly HospitalDBContext _context;

        public PatientsController(HospitalDBContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
          if (_context.Patients == null)
          {
              return NotFound();
          }
            return await _context.Patients.Where(x => x.IsDeleted == false).ToListAsync();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
          if (_context.Patients == null)
          {
              return NotFound();
          }
            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.Id == id && m.IsDeleted == false);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, Patient patientData)
        {
            if (patientData == null || id == 0)
                return BadRequest();

            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
                return NotFound();

            patient.FirstName = patientData.FirstName ?? patient.FirstName;
            patient.LastName = patientData.LastName ?? patient.LastName;
            patient.Address = patientData.Address ?? patient.Address;
            patient.State = patientData.State ?? patient.State;
            patient.City = patientData.City ?? patient.City;
            patient.OrganizationId = patientData.OrganizationId;
            patient.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Patients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
          if (_context.Patients == null)
          {
              return Problem("Entity set 'Patients'  is null.");
          }
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            if (_context.Patients == null)
            {
                return NotFound();
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            patient.IsDeleted = true;
            //_context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }
       
    }
}
