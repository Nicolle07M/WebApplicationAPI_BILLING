using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationAPI_BILLING.Data;
using WebApplicationAPI_BILLING.Models;

namespace WebApplicationAPI_BILLING.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesCController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdenesCController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ordenCs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenC>>> GetordenCs()
        {
            if (_context.OrdenesC == null)
            {
                return NotFound();
            }
            return await _context.OrdenesC.Include(oi => oi.OrdenItems).ToListAsync();
        }

        // GET: api/ordenCs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenC>> GetordenC(int id)
        {
            if (_context.OrdenesC == null)
            {
                return NotFound();
            }
            // var ordenC = await _context.ordenCs.Include(oi => oi.ordenCItems).FindAsync(id);
            var ordenC = await _context.OrdenesC.Include(oi => oi.OrdenItems)
                                     .FirstOrDefaultAsync(o => o.Id == id); // Asumiendo que el nombre del campo es 'Id'.

            if (ordenC == null)
            {
                return NotFound();
            }

            return ordenC;
        }

        // PUT: api/ordenCs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutordenC(int id, OrdenC ordenC)
        {
            if (id != ordenC.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordenC).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ordenCExists(id))
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

        // POST: api/ordenCs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenC>> PostordenC(OrdenC ordenC)
        {
            if (_context.OrdenesC == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ordenCs'  is null.");
            }
            _context.OrdenesC.Add(ordenC);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetordenC", new { id = ordenC.Id }, ordenC);
        }

        // DELETE: api/ordenCs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteordenC(int id)
        {
            if (_context.OrdenesC == null)
            {
                return NotFound();
            }
            var ordenC = await _context.OrdenesC.FindAsync(id);
            if (ordenC == null)
            {
                return NotFound();
            }

            _context.OrdenesC.Remove(ordenC);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ordenCExists(int id)
        {
            return (_context.OrdenesC?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}