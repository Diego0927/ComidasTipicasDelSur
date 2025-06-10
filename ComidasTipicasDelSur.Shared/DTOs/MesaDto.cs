using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Shared.DTOs
{
    public class MesaDto
    {
        public int NroMesa { get; set; }
        public string? Nombre { get; set; }
        public char Reservada { get; set; }
        public int Puestos { get; set; }
    }
}
