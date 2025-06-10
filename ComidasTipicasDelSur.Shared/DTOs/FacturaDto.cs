using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Shared.DTOs
{
    public class FacturaDto
    {
        public int NroFactura { get; set; }
        public string? IdCliente { get; set; }
        public int NroMesa { get; set; }
        public int IdMesero { get; set; }
        public DateTime Fecha { get; set; }
    }
}