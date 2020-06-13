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
    public class Relacion_Personaje_CategoriaController : ControllerBase
    {
        private readonly NoLosOlvidesApiContext _context;

        public Relacion_Personaje_CategoriaController(NoLosOlvidesApiContext context)
        {
            _context = context;
        }

        // GET: api/Relacion_Personaje_Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relacion_Personaje_Categoria>>> GetRelacion_Personaje_Categoria()
        {
            return await _context.Relacion_Personaje_Categoria.ToListAsync();
        }

        // GET: api/Relacion_Personaje_Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Relacion_Personaje_Categoria>> GetRelacion_Personaje_Categoria(int id)
        {
            var relacion_Personaje_Categoria = await _context.Relacion_Personaje_Categoria.FindAsync(id);

            if (relacion_Personaje_Categoria == null)
            {
                return NotFound();
            }

            return relacion_Personaje_Categoria;
        }

        // PUT: api/Relacion_Personaje_Categoria/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelacion_Personaje_Categoria(int id, Relacion_Personaje_Categoria relacion_Personaje_Categoria)
        {
            if (id != relacion_Personaje_Categoria.IdRelacionPersonajeCategoria)
            {
                return BadRequest();
            }

            _context.Entry(relacion_Personaje_Categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Relacion_Personaje_CategoriaExists(id))
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

        // POST: api/Relacion_Personaje_Categoria
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Relacion_Personaje_Categoria>> PostRelacion_Personaje_Categoria(Relacion_Personaje_Categoria relacion_Personaje_Categoria)
        {
            _context.Relacion_Personaje_Categoria.Add(relacion_Personaje_Categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelacion_Personaje_Categoria", new { id = relacion_Personaje_Categoria.IdRelacionPersonajeCategoria }, relacion_Personaje_Categoria);
        }

        // DELETE: api/Relacion_Personaje_Categoria/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Relacion_Personaje_Categoria>> DeleteRelacion_Personaje_Categoria(int id)
        {
            var relacion_Personaje_Categoria = await _context.Relacion_Personaje_Categoria.FindAsync(id);
            if (relacion_Personaje_Categoria == null)
            {
                return NotFound();
            }

            _context.Relacion_Personaje_Categoria.Remove(relacion_Personaje_Categoria);
            await _context.SaveChangesAsync();

            return relacion_Personaje_Categoria;
        }

        private bool Relacion_Personaje_CategoriaExists(int id)
        {
            return _context.Relacion_Personaje_Categoria.Any(e => e.IdRelacionPersonajeCategoria == id);
        }
    }
}
