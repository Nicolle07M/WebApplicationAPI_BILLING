﻿using WebApplicationAPI_BILLING.Data;
using WebApplicationAPI_BILLING.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

 namespace WebApplicationAPI_BILLING.Controllers
{
    [Route("api/[controller]")] // Framework de ruteo. Ámbito Global
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProveedoresController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: api/<ProveedoresController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> Get()
        {
            if (_context.Proveedores == null)
            {
                return NotFound();
            }
            return await _context.Proveedores.ToListAsync();
        }


        // GET api/<ProveedoresController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> Get(int id)
        {
            if (_context.Proveedores == null)
            {
                return NotFound();
            }
            var supplier = await _context.Proveedores.FindAsync(id);

            if (supplier is null)
            {
                return NotFound();
            }

            return supplier;
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public async Task<ActionResult<Proveedor>> Post([FromBody] Proveedor supplier)
        {
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Suppliers'  is null.");
            }
            _context.Proveedores.Add(supplier);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = supplier.Id }, supplier);
        }

        // PUT api/<SuppliersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Proveedor supplier)
        {

            if (id != supplier.Id)
            {
                return BadRequest();
            }
            _context.Entry(supplier).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!SupplierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
            return NoContent();
        }

        private bool SupplierExists(int id)
        {
            return (_context.Proveedores?.Any(s => s.Id == id)).GetValueOrDefault();
        }

        // DELETE api/<SuppliersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Proveedores is null)
            {
                return NotFound();
            }

            var supplier = await _context.Proveedores.FirstOrDefaultAsync(s => s.Id == id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(supplier);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}