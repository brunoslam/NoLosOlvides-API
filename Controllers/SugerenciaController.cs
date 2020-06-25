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
    public class SugerenciaController : ControllerBase
    {
        private readonly NoLosOlvidesApiContext _context;

        public SugerenciaController(NoLosOlvidesApiContext context)
        {
            _context = context;
        }

        // GET: api/Sugerencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sugerencia>>> GetSugerencia()
        {
            return await _context.Sugerencia.ToListAsync();
        }

        // GET: api/Sugerencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sugerencia>> GetSugerencia(int id)
        {
            var sugerencia = await _context.Sugerencia.FindAsync(id);

            if (sugerencia == null)
            {
                return NotFound();
            }

            return sugerencia;
        }

        // PUT: api/Sugerencia/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSugerencia(int id, Sugerencia sugerencia)
        {
            if (id != sugerencia.IdSugerencia)
            {
                return BadRequest();
            }

            _context.Entry(sugerencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SugerenciaExists(id))
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

        // POST: api/Sugerencia
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Sugerencia>> PostSugerencia(Sugerencia sugerencia)
        {
            _context.Sugerencia.Add(sugerencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSugerencia", new { id = sugerencia.IdSugerencia }, sugerencia);
        }

        // DELETE: api/Sugerencia/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sugerencia>> DeleteSugerencia(int id)
        {
            var sugerencia = await _context.Sugerencia.FindAsync(id);
            if (sugerencia == null)
            {
                return NotFound();
            }

            _context.Sugerencia.Remove(sugerencia);
            await _context.SaveChangesAsync();

            return sugerencia;
        }

        private bool SugerenciaExists(int id)
        {
            return _context.Sugerencia.Any(e => e.IdSugerencia == id);
        }
    }
}
