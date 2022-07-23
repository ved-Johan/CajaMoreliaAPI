using System;
using System.Collections.Generic;

namespace CajaMorelia.Models
{
    public partial class Cliente    
    {
        public Cliente()
        {
            ClienteCuenta = new HashSet<ClienteCuenta>();
        }

        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string Rfc { get; set; } = null!;
        public string Curp { get; set; } = null!;
        public DateTime? FechaAlta { get; set; }

        public virtual ICollection<ClienteCuenta> ClienteCuenta { get; set; }
    }
}
