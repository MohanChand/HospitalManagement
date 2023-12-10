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
    public class VisitorsController : ControllerBase
    {
        private readonly HospitalDBContext _context;

        public VisitorsController(HospitalDBContext context)
        {
            _context = context;
        }

        // GET: api/Visitors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitors()
        {
            if (_context.Visitors == null)
            {
                return NotFound();
            }
            return await _context.Visitors.ToListAsync();
        }

        // GET: api/Visitors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> GetVisitor(int id)
        {
            if (_context.Visitors == null)
            {
                return NotFound();
            }
            var visitor = await _context.Visitors.FindAsync(id);

            if (visitor == null)
            {
                return NotFound();
            }

            return visitor;
        }

        // PUT: api/Visitors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitor(int id, Visitor visitorData)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var visitor = await _context.Visitors.FindAsync(id);

            if (visitor == null)
                return NotFound();

            visitor.PatientID = visitorData.PatientID ?? visitor.PatientID;
            visitor.VisitDate = visitorData.VisitDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Visitors
        [HttpPost]
        public async Task<ActionResult<Visitor>> PostVisitor(Visitor visitor)
        {
            if (_context.Visitors == null)

            {
                return Problem("Entity set 'Visitors'  is null.");
            }
            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVisitor", new { id = visitor.Id }, visitor);
        }

        // DELETE: api/Visitors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            if (_context.Visitors == null)
            {
                return NotFound();
            }
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }

            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VisitorExists(int id)
        {
            return (_context.Visitors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
