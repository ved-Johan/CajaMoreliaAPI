using System;
using System.Collections.Generic;

namespace CajaMorelia.Models
{
    public partial class TipoCuenta
    {
        public TipoCuenta()
        {
            ClienteCuenta = new HashSet<ClienteCuenta>();
        }

        public int IdCuenta { get; set; }
        public string NombreCuenta { get; set; } = null!;

        public virtual ICollection<ClienteCuenta> ClienteCuenta { get; set; }
    }
}
