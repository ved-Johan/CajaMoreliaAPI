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
    public class ClienteCuentasController : ControllerBase
    {
        private readonly DBContext _context;

        public ClienteCuentasController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ClienteCuentas/5
        // GET todas las cuentas utilizando el id de un cliente
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ClienteCuenta>>> GetClienteCuenta(int id)
        {
          if (_context.ClienteCuenta == null)
          {
              return NotFound();
          }
            var clienteCuenta = await _context.ClienteCuenta.FromSqlInterpolated($"CALL GetCuentas({id})").ToListAsync();

            if (clienteCuenta == null)
            {
                return NotFound();
            }

            return clienteCuenta;
        }

        // PUT: api/ClienteCuentas/5
        // Actualiza el saldo de una cuenta
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClienteCuenta(int id, ClienteCuenta clienteCuenta)
        {
            if (id != clienteCuenta.IdClienteCuenta)
            {
                return BadRequest();
            }

            _context.Database.ExecuteSqlInterpolated($"CALL UpdateSaldo({id}, {clienteCuenta.SaldoActual})");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteCuentaExists(id))
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

        // POST: api/ClienteCuentas
        // Crea una cuenta y regresa la cuenta creada
        [HttpPost]
        public async Task<ActionResult<ClienteCuenta>> PostClienteCuenta(ClienteCuenta clienteCuenta)
        {
          if (_context.ClienteCuenta == null)
          {
              return Problem("Entity set 'DBContext.ClienteCuenta'  is null.");
          }
            var insertedCuenta = _context.ClienteCuenta.FromSqlInterpolated($"CALL InsertCuenta({clienteCuenta.IdCliente}, {clienteCuenta.IdCuenta}, {clienteCuenta.SaldoActual})").ToList().SingleOrDefault();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteCuentaExists(clienteCuenta.IdClienteCuenta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return insertedCuenta;
        }

        // DELETE: api/ClienteCuentas/5
        // Elimina la cuenta con el id asociado
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClienteCuenta(int id)
        {
            if (_context.ClienteCuenta == null)
            {
                return NotFound();
            }

            _context.Database.ExecuteSqlInterpolated($"CALL DeleteCuenta({id})");
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteCuentaExists(int id)
        {
            return (_context.ClienteCuenta?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
