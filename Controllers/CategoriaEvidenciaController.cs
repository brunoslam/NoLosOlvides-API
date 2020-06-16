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
    public class CategoriaEvidenciaController : ControllerBase
    {
        private readonly NoLosOlvidesApiContext _context;

        public CategoriaEvidenciaController(NoLosOlvidesApiContext context)
        {
            _context = context;
        }

        // GET: api/CategoriaEvidencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaEvidencia>>> GetCategoriaEvidencia()
        {
            return await _context.CategoriaEvidencia.ToListAsync();
        }

        // GET: api/CategoriaEvidencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaEvidencia>> GetCategoriaEvidencia(int id)
        {
            var categoriaEvidencia = await _context.CategoriaEvidencia.FindAsync(id);

            if (categoriaEvidencia == null)
            {
                return NotFound();
            }

            return categoriaEvidencia;
        }

        // PUT: api/CategoriaEvidencia/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaEvidencia(int id, CategoriaEvidencia categoriaEvidencia)
        {
            if (id != categoriaEvidencia.IdCategoriaEvidencia)
            {
                return BadRequest();
            }

            _context.Entry(categoriaEvidencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaEvidenciaExists(id))
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

        // POST: api/CategoriaEvidencia
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CategoriaEvidencia>> PostCategoriaEvidencia(CategoriaEvidencia categoriaEvidencia)
        {
            
            try
            {
                if (_context.CategoriaEvidencia.Where(c => c.Titulo == categoriaEvidencia.Titulo).Count() > 0)
                {
                    return BadRequest(new { message = "Ya existen registros con esa información" });
                }
                _context.CategoriaEvidencia.Add(categoriaEvidencia);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCategoriaEvidencia", new { id = categoriaEvidencia.IdCategoriaEvidencia }, categoriaEvidencia);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/CategoriaEvidencia/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaEvidencia>> DeleteCategoriaEvidencia(int id)
        {
            var categoriaEvidencia = await _context.CategoriaEvidencia.FindAsync(id);
            if (categoriaEvidencia == null)
            {
                return NotFound();
            }

            _context.CategoriaEvidencia.Remove(categoriaEvidencia);
            await _context.SaveChangesAsync();

            return categoriaEvidencia;
        }

        private bool CategoriaEvidenciaExists(int id)
        {
            return _context.CategoriaEvidencia.Any(e => e.IdCategoriaEvidencia == id);
        }
    }
}
