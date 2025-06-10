using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Shared.DTOs
{
    public class ClienteTopDto
    {
        public string Identificacion { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public decimal TotalConsumo { get; set; }
    }
}
