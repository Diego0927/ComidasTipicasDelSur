using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Shared.DTOs
{
    public class DetalleFacturaDto
    {
        public int IdDetalleXFactura { get; set; }
        public int NroFactura { get; set; }
        public int IdSupervisor { get; set; }
        public string? Plato { get; set; }
        public decimal Valor { get; set; }
    }
}
