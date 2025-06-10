using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Domain.Entities
{
    public class Supervisor
    {
        [Key]
        public int IdSupervisor { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int Edad { get; set; }
        public int Antiguedad { get; set; }

        public ICollection<DetalleFactura> DetallesFactura { get; set; } = [];
    }
}

