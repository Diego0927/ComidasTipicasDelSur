using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Domain.Entities
{
    public class DetalleFactura
    {
        [Key]
        public int IdDetalleXFactura { get; set; }
        public int NroFactura { get; set; }
        public int IdSupervisor { get; set; }
        public string Plato { get; set; } = null!;
        public decimal Valor { get; set; }

        public Factura Factura { get; set; } = null!;
        public Supervisor Supervisor { get; set; } = null!;
    }
}
