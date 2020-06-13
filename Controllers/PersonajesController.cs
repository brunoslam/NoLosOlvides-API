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
        public async Task<ActionResult<IEnumerable<Personaje>>> GetPersonajesAprobados()
        {
            return await _context.Personaje.Where(p => p.IdEstadoAprobacion == 2).ToListAsync();
        }
        // GET: api/Personajes
        [HttpGet("pendientes")]
        public async Task<ActionResult<IEnumerable<Personaje>>> GetPersonajesPendientes()
        {
            return await _context.Personaje.Where(p => p.IdEstadoAprobacion == 1).ToListAsync();
        }
        // GET: api/Personajes
        [HttpGet("aprobados")]
        public async Task<ActionResult<IEnumerable<Personaje>>> GetPersonajes()
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
            //var CargoController = new CargosController(_context);
            //Cargo cargo = _context.Cargo.Find(personaje.IdCargo);
            //personaje.Cargo = cargo;


            if (personaje == null)
            {
                return NotFound();
            }

            List<Relacion_Personaje_Cargo> relacion_Personaje_Cargos = await _context.Relacion_Personaje_Cargo.Where(r => r.IdPersonaje == personaje.IdPersonaje).ToListAsync();
            personaje.ArrRelacionCargo = relacion_Personaje_Cargos;
            List<Relacion_Personaje_Categoria> relacion_Personaje_Categorias = await _context.Relacion_Personaje_Categoria.Where(r => r.IdPersonaje == personaje.IdPersonaje).ToListAsync();
            personaje.ArrRelacionCategoria = relacion_Personaje_Categorias;
            List<Relacion_Personaje_Categoria> relacion_Personaje_Evidencias = await _context.Relacion_Personaje_Categoria.Where(r => r.IdPersonaje == personaje.IdPersonaje).ToListAsync();
            personaje.ArrRelacionCategoria = relacion_Personaje_Categorias;

            //Comentar si es necesario
            personaje.ArrCargo = new List<Cargo>();
            foreach (Relacion_Personaje_Cargo relacion_cargo in relacion_Personaje_Cargos)
            {
                var xasd = _context.Cargo.Where(c => c.IdCargo == relacion_cargo.IdCargo).FirstOrDefault();
                personaje.ArrCargo.Add(_context.Cargo.Where(c => c.IdCargo == relacion_cargo.IdCargo).FirstOrDefault());

            }
            personaje.ArrCategoria = new List<Categoria>();
            foreach (Relacion_Personaje_Categoria relacion_categoria in relacion_Personaje_Categorias)
            {
                personaje.ArrCategoria.Add(_context.Categoria.Where(c => c.IdCategoria == relacion_categoria.IdCategoria).FirstOrDefault());
            }
            //
            personaje.ArrEvidencias = await _context.Evidencia.Where(e => e.IdPersonaje == personaje.IdPersonaje).ToListAsync();


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

                try
                {
                    //Insertar personaje
                    _context.Personaje.Add(personaje);
                    await _context.SaveChangesAsync();
                    //insertar evidencia
                    foreach (Evidencia evidencia in personaje.ArrEvidencias)
                    {
                        evidencia.IdPersonaje = personaje.IdPersonaje;
                        _context.Evidencia.Add(evidencia);
                    }
                    await _context.SaveChangesAsync();

                    //insertar Relacion_Personaje_Cargo
                    foreach (Cargo cargo in personaje.ArrCargo)
                    {
                        Relacion_Personaje_Cargo relacion_Personaje_Cargo = new Relacion_Personaje_Cargo
                        {
                            IdCargo = cargo.IdCargo,
                            IdPersonaje = personaje.IdPersonaje
                        };
                        _context.Relacion_Personaje_Cargo.Add(relacion_Personaje_Cargo);
                    }
                    await _context.SaveChangesAsync();

                    //insertar Relacion_Personaje_Categoria
                    foreach (Categoria categoria in personaje.ArrCategoria)
                    {
                        Relacion_Personaje_Categoria relacion_Personaje_Categoria = new Relacion_Personaje_Categoria
                        {
                            IdCategoria = categoria.IdCategoria,
                            IdPersonaje = personaje.IdPersonaje
                        };
                        _context.Relacion_Personaje_Categoria.Add(relacion_Personaje_Categoria);
                    }
                    await _context.SaveChangesAsync();

                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return BadRequest(new { message = "Error al insertar" });
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
