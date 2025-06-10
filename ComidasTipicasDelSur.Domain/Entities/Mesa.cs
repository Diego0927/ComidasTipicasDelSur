using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Domain.Entities
{
    public class Mesa
    {
        [Key]
        public int NroMesa { get; set; }
        public string Nombre { get; set; } = null!;
        public char Reservada { get; set; }
        public int Puestos { get; set; }

        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}

