using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Shared.DTOs
{
    public class MeseroDto
    {
        public int IdMesero { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public int Edad { get; set; }
        public int Antiguedad { get; set; }
    }
}
