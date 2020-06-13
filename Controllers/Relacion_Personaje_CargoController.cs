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
    public class Relacion_Personaje_CargoController : ControllerBase
    {
        private readonly NoLosOlvidesApiContext _context;

        public Relacion_Personaje_CargoController(NoLosOlvidesApiContext context)
        {
            _context = context;
        }

        // GET: api/Relacion_Personaje_Cargo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Relacion_Personaje_Cargo>>> GetRelacion_Personaje_Cargo()
        {
            return await _context.Relacion_Personaje_Cargo.ToListAsync();
        }

        // GET: api/Relacion_Personaje_Cargo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Relacion_Personaje_Cargo>> GetRelacion_Personaje_Cargo(int id)
        {
            var relacion_Personaje_Cargo = await _context.Relacion_Personaje_Cargo.FindAsync(id);

            if (relacion_Personaje_Cargo == null)
            {
                return NotFound();
            }

            return relacion_Personaje_Cargo;
        }

        // PUT: api/Relacion_Personaje_Cargo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelacion_Personaje_Cargo(int id, Relacion_Personaje_Cargo relacion_Personaje_Cargo)
        {
            if (id != relacion_Personaje_Cargo.IdRelacionPersonajeCargo)
            {
                return BadRequest();
            }

            _context.Entry(relacion_Personaje_Cargo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Relacion_Personaje_CargoExists(id))
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

        // POST: api/Relacion_Personaje_Cargo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Relacion_Personaje_Cargo>> PostRelacion_Personaje_Cargo(Relacion_Personaje_Cargo relacion_Personaje_Cargo)
        {
            _context.Relacion_Personaje_Cargo.Add(relacion_Personaje_Cargo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelacion_Personaje_Cargo", new { id = relacion_Personaje_Cargo.IdRelacionPersonajeCargo }, relacion_Personaje_Cargo);
        }

        // DELETE: api/Relacion_Personaje_Cargo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Relacion_Personaje_Cargo>> DeleteRelacion_Personaje_Cargo(int id)
        {
            var relacion_Personaje_Cargo = await _context.Relacion_Personaje_Cargo.FindAsync(id);
            if (relacion_Personaje_Cargo == null)
            {
                return NotFound();
            }

            _context.Relacion_Personaje_Cargo.Remove(relacion_Personaje_Cargo);
            await _context.SaveChangesAsync();

            return relacion_Personaje_Cargo;
        }

        private bool Relacion_Personaje_CargoExists(int id)
        {
            return _context.Relacion_Personaje_Cargo.Any(e => e.IdRelacionPersonajeCargo == id);
        }
    }
}
