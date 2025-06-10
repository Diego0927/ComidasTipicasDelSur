using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Application.Interfaces
{
    public interface IFacturaService
    {
        Task<IEnumerable<FacturaDto>> ObtenerTodosAsync();
        Task<FacturaDto?> ObtenerPorIdAsync(int nrofactura);
        Task CrearAsync(FacturaDto factura);
        Task ActualizarAsync(FacturaDto factura);
        Task EliminarAsync(int nrofactura);
    }
}
