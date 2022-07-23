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
    public class TipoCuentasController : ControllerBase
    {
        private readonly DBContext _context;

        public TipoCuentasController(DBContext context)
        {
            _context = context;
        }

        // GET: api/TipoCuentas
        // Regresa todos los tipos de cuentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCuenta>>> GetTipoCuenta()
        {
            if (_context.TipoCuenta == null)
            {
                return NotFound();
            }
            return await _context.TipoCuenta.FromSqlInterpolated($"CALL GetTipos()").ToListAsync();
        }
    }
}
