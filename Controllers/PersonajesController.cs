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
    public class PersonajesController : ControllerBase
    {
        private readonly NoLosOlvidesApiContext _context;

        public PersonajesController(NoLosOlvidesApiContext context)
        {
            _context = context;
        }

        // GET: api/Personajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personaje>>> GetPersonaje()
        {
            return await _context.Personaje.ToListAsync();
        }

        // GET: api/Personajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personaje>> GetPersonaje(int id)
        {
            var personaje = await _context.Personaje.FindAsync(id);

            if (personaje == null)
            {
                return NotFound();
            }

            return personaje;
        }

        // PUT: api/Personajes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaje(int id, Personaje personaje)
        {
            if (id != personaje.IdPersonaje)
            {
                return BadRequest();
            }

            _context.Entry(personaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajeExists(id))
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

        // POST: api/Personajes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Personaje>> PostPersonaje(Personaje personaje)
        {
            _context.Personaje.Add(personaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonaje", new { id = personaje.IdPersonaje }, personaje);
        }

        // DELETE: api/Personajes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personaje>> DeletePersonaje(int id)
        {
            var personaje = await _context.Personaje.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _context.Personaje.Remove(personaje);
            await _context.SaveChangesAsync();

            return personaje;
        }

        private bool PersonajeExists(int id)
        {
            return _context.Personaje.Any(e => e.IdPersonaje == id);
        }
    }
}
