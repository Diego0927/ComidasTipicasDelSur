using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Shared.DTOs
{
    public class ProductoMasVendidoDto
    {
        public string Plato { get; set; } = null!;
        public int CantidadVendida { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
