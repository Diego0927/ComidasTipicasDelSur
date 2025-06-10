using ComidasTipicasDelSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Interfaces
{
    public interface IDetalleFacturaRepository
    {
        Task<IEnumerable<DetalleFactura>> ObtenerTodosAsync();
        Task<DetalleFactura?> ObtenerPorIdAsync(int iddetallexfactura);
        Task CrearAsync(DetalleFactura iddetallexfactura);
        Task ActualizarAsync(DetalleFactura iddetallexfactura);
        Task EliminarAsync(int iddetallexfactura);
        Task<DetalleFactura?> ObtenerFacturasConDetallesAsync(int iddetallexfactura);
    }
}
