using ComidasTipicasDelSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> ObtenerTodosAsync();
        Task<Factura?> ObtenerPorIdAsync(int nrofactura);
        Task CrearAsync(Factura factura);
        Task ActualizarAsync(Factura factura);
        Task EliminarAsync(int nrofactura);
    }
}
