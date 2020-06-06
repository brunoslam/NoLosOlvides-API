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
    public class StaffController : ControllerBase
    {
        private readonly NoLosOlvidesApiContext _context;

        public StaffController(NoLosOlvidesApiContext context)
        {
            _context = context;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            return await _context.Staff.ToListAsync();
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // PUT: api/Staff/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStaff(int id, Staff staff)
        {
            if (id != staff.IdStaff)
            {
                return BadRequest();
            }

            _context.Entry(staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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

        // POST: api/Staff
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
       [HttpPost]
        public async Task<ActionResult<Staff>> PostStaff(Staff staff)
        {
            _context.Staff.Add(staff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStaff", new { id = staff.IdStaff }, staff);
        }

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Staff>> DeleteStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return staff;
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.IdStaff == id);
        }


        // POST: api/Staff
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost("{Username}")]
        [HttpPost]
        //public async Task<ActionResult<Staff>> StaffLogin([FromBody] Staff staffFB)
        public async Task<ActionResult<Staff>> StaffLogin(string Username)
        {
            //Staff staff = _context.Staff.Where(s => s.Username == username && s.Password == password).First();
            Staff staff = _context.Staff.Where(s => s.Username == Username).First();
            //Staff staff = _context.Staff.Where(s => s.Username == staffFB.Username ).First();
            //_context.Staff.Add(staff);
            //await _context.SaveChangesAsync();
            if (staff == null)
            {
                return NotFound();
            }

            //return CreatedAtAction("GetStaff", new { id = staff.Id }, staff);
            return staff;
        }
    }
}
