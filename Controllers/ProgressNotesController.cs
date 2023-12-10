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
    public class ProgressNotesController : ControllerBase
    {
        private readonly HospitalDBContext _context;

        public ProgressNotesController(HospitalDBContext context)
        {
            _context = context;
        }

        // GET: api/ProgressNotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgressNote>>> GetProgressNotes()
        {
          if (_context.ProgressNotes == null)
          {
              return NotFound();
          }
            return await _context.ProgressNotes.ToListAsync();
        }

        // GET: api/ProgressNotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgressNote>> GetProgressNote(int id)
        {
          if (_context.ProgressNotes == null)
          {
              return NotFound();
          }
            var progressNote = await _context.ProgressNotes.FindAsync(id);

            if (progressNote == null)
            {
                return NotFound();
            }

            return progressNote;
        }

        // PUT: api/ProgressNotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgressNote(int id, ProgressNote progressNoteData)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var progressNote = await _context.ProgressNotes.FindAsync(id);

            if (progressNote == null)
                return NotFound();

            progressNote.Height = progressNoteData.Height ?? progressNote.Height;
            progressNote.Weight = progressNoteData.Weight ?? progressNote.Weight;
            progressNote.Temperature = progressNoteData.Temperature ?? progressNote.Temperature;
            progressNote.PatientId = progressNoteData.PatientId ?? progressNote.PatientId;
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProgressNotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProgressNote>> PostProgressNote(ProgressNote progressNote)
        {
          if (_context.ProgressNotes == null)
          {
              return Problem("Entity set 'ProgressNotes'  is null.");
          }
            _context.ProgressNotes.Add(progressNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgressNote", new { id = progressNote.Id }, progressNote);
        }

        // DELETE: api/ProgressNotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgressNote(int id)
        {
            if (_context.ProgressNotes == null)
            {
                return NotFound();
            }
            var progressNote = await _context.ProgressNotes.FindAsync(id);
            if (progressNote == null)
            {
                return NotFound();
            }

            _context.ProgressNotes.Remove(progressNote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgressNoteExists(int id)
        {
            return (_context.ProgressNotes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
