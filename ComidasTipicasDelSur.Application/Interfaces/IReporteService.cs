using ComidasTipicasDelSur.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Application.Interfaces
{
    public interface IReporteService
    {
        Task<IEnumerable<VentaMeseroDto>> ObtenerVentasPorMeseroAsync(DateTime inicio, DateTime fin);
        Task<IEnumerable<ClienteTopDto>> ObtenerClientesPorConsumoMinimoAsync(decimal minimo);
        Task<ProductoMasVendidoDto?> ObtenerProductoMasVendidoAsync(DateTime inicio, DateTime fin);
    }
}
