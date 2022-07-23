using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CajaMorelia.Data;
using CajaMorelia.Models;

namespace CajaMoreliaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly DBContext _context;

        public ClientesController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
          if (_context.Cliente == null)
          {
              return NotFound();
          }
            return await _context.Cliente.FromSqlRaw("CALL GetAllClientes()").ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
          if (_context.Cliente == null)
          {
              return NotFound();
          }
            var cliente = _context.Cliente.FromSqlInterpolated($"CALL GetClienteById({id})").ToList().SingleOrDefault();

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }
            _context.Cliente.FromSqlInterpolated($"CALL UpdateCliente({cliente.Nombre}, {cliente.ApellidoPaterno}, {cliente.ApellidoMaterno}, {cliente.Rfc}, {cliente.Curp}, {id})").ToList().SingleOrDefault();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
          if (_context.Cliente == null)
          {
              return Problem("Entity set 'DBContext.Cliente'  is null.");
          }
            var insertedCliente = _context.Cliente.FromSqlInterpolated($"CALL InsertCliente({cliente.Nombre}, {cliente.ApellidoPaterno}, {cliente.ApellidoMaterno}, {cliente.Rfc}, {cliente.Curp})").ToList().SingleOrDefault();
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", insertedCliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = _context.Cliente.FromSqlInterpolated($"CALL GetClienteById({id})").ToList().SingleOrDefault();
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"CALL DeleteCliente({id})");
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Cliente?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
