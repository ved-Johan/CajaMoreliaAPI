using System;
using System.Collections.Generic;

namespace CajaMorelia.Models
{
    public partial class ClienteCuenta
    {
        public int IdClienteCuenta { get; set; }
        public int IdCliente { get; set; }
        public int IdCuenta { get; set; }
        public decimal SaldoActual { get; set; }
        public DateTime FechaContratacion { get; set; }
        public DateTime? FechaUltimoMovimiento { get; set; }

        // public virtual Cliente IdClienteNavigation { get; set; } = null!;
        // public virtual TipoCuenta IdCuentaNavigation { get; set; } = null!;
    }
}
