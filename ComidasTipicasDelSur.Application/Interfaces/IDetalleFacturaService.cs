using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Application.Interfaces
{
    public interface IDetalleFacturaService
    {
        Task<IEnumerable<DetalleFacturaDto>> ObtenerTodosAsync();
        Task<DetalleFacturaDto?> ObtenerPorIdAsync(int iddetallexfactura);
        Task CrearAsync(DetalleFacturaDto iddetallexfactura);
        Task ActualizarAsync(DetalleFacturaDto iddetallexfactura);
        Task<(bool success, string message)> EliminarAsync(int iddetallexfactura);
    }
}
