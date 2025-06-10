using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Domain.Entities
{
    public class Factura
    {
        [Key]
        public int NroFactura { get; set; }
        public string IdCliente { get; set; } = null!;
        public int NroMesa { get; set; }
        public int IdMesero { get; set; }
        public DateTime Fecha { get; set; }

        public Cliente Cliente { get; set; } = null!;
        public Mesa Mesa { get; set; } = null!;
        public Mesero Mesero { get; set; } = null!;
        public ICollection<DetalleFactura> Detalles { get; set; } = [];
    }
}
