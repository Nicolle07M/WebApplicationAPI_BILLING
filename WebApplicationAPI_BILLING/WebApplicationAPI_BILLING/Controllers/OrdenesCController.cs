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

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenC>>> GetOrders()
        {
            if (_context.OrdenesC == null)
            {
                return NotFound();
            }
            return await _context.OrdenesC.Include(oi => oi.OrdenItems).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenC>> GetOrder(int id)
        {
            if (_context.OrdenesC == null)
            {
                return NotFound();
            }
            // var order = await _context.Orders.Include(oi => oi.OrderItems).FindAsync(id);
            var order = await _context.OrdenesC.Include(oi => oi.OrdenItems)
                                     .FirstOrDefaultAsync(o => o.Id == id); // Asumiendo que el nombre del campo es 'Id'.

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrdenC order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdenC>> PostOrder(OrdenC order)
        {
            if (_context.OrdenesC == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            _context.OrdenesC.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_context.OrdenesC == null)
            {
                return NotFound();
            }
            var order = await _context.OrdenesC.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.OrdenesC.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return (_context.OrdenesC?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}