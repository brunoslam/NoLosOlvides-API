using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoLosOlvidesApi.Data;
using NoLosOlvidesApi.Model;

namespace NoLosOlvidesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvidenciaController : ControllerBase
    {
        private readonly NoLosOlvidesApiContext _context;

        public EvidenciaController(NoLosOlvidesApiContext context)
        {
            _context = context;
        }

        // GET: api/Evidencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evidencia>>> GetEvidencia()
        {
            return await _context.Evidencia.ToListAsync();
        }

        // GET: api/Evidencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evidencia>> GetEvidencia(int id)
        {
            var evidencia = await _context.Evidencia.FindAsync(id);

            if (evidencia == null)
            {
                return NotFound();
            }

            return evidencia;
        }

        // PUT: api/Evidencia/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvidencia(int id, Evidencia evidencia)
        {
            if (id != evidencia.IdEvidencia)
            {
                return BadRequest();
            }

            _context.Entry(evidencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvidenciaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Evidencia
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Evidencia>> PostEvidencia(Evidencia evidencia)
        {
            _context.Evidencia.Add(evidencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvidencia", new { id = evidencia.IdEvidencia }, evidencia);
        }

        // DELETE: api/Evidencia/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evidencia>> DeleteEvidencia(int id)
        {
            var evidencia = await _context.Evidencia.FindAsync(id);
            if (evidencia == null)
            {
                return NotFound();
            }

            _context.Evidencia.Remove(evidencia);
            await _context.SaveChangesAsync();

            return evidencia;
        }

        private bool EvidenciaExists(int id)
        {
            return _context.Evidencia.Any(e => e.IdEvidencia == id);
        }
    }
}
