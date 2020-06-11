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
            //var personaje = _context.Personaje.Where(p => p.IdPersonaje == id).Include(p => p.IdCargo.se).FirstOrDefault();
            //var personaje = await _context.Personaje.FindAsync(id);
            var personaje = _context.Personaje.Where(p => p.IdPersonaje == id).FirstOrDefault();
            var CargoController = new CargosController(_context);
            var asd = _context.Cargo.Find(personaje.IdCargo);
            personaje.Cargo = asd;

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
        public async Task<ActionResult<Personaje>> PostPersonaje([FromBody]Personaje personaje)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                bool flagResultado = (await GetPersonajePorNombre(personaje)).Count() > 0;

                if (flagResultado)
                    return BadRequest(new { message = "Ya existen registros con esa información" });


                _context.Personaje.Add(personaje);



                await _context.SaveChangesAsync();
                try
                {
                    foreach (Evidencia evidencia in personaje.ArrEvidencias)
                    {
                        evidencia.IdPersonaje = personaje.IdPersonaje;
                        _context.Evidencia.Add(evidencia);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
                dbContextTransaction.Commit();

            }
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

        // GET: api/Personajes/
        [HttpGet("top")]
        public async Task<ActionResult<IEnumerable<Personaje>>> GetTopPersonajes()
        {
            return await _context.Personaje.Take(10).ToListAsync();
        }

        // Post: api/Personajes/
        [HttpPost("BuscarPorNombre")]
        public async Task<ActionResult<IEnumerable<Personaje>>> CheckPersonajePorNombre([FromBody]Personaje personaje)
        {
            IEnumerable<Personaje> resultados = await GetPersonajePorNombre(personaje);

            if (resultados.Count() > 0)
                return BadRequest(new { message = "Ya existen registros con esa información" });

            return Ok(resultados);

        }

        public async Task<IEnumerable<Personaje>> GetPersonajePorNombre(Personaje personaje)
        {
            IEnumerable<Personaje> resultados = await _context.Personaje.Where(p => p.Nombre.Contains(personaje.Nombre) && p.Apellido.Contains(personaje.Apellido)).ToListAsync();
            return resultados;
        }

    }
}
