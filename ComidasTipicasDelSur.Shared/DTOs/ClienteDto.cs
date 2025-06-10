using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Shared.DTOs
{
    public class ClienteDto
    {
        public string? Identificacion { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}
