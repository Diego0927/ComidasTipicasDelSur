using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Domain.Entities
{
    public class Cliente
    {
        [Key]
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public ICollection<Factura> Facturas { get; set; } = [];
    }
}
