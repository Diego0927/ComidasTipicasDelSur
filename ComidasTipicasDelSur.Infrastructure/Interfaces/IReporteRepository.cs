using ComidasTipicasDelSur.Shared.DTOs;

namespace ComidasTipicasDelSur.Infrastructure.Interfaces
{
    public interface IReporteRepository
    {
        Task<IEnumerable<VentaMeseroDto>> ObtenerVentasPorMeseroAsync(DateTime inicio, DateTime fin);
        Task<IEnumerable<ClienteTopDto>> ObtenerClientesPorConsumoMinimoAsync(decimal minimo);
        Task<ProductoMasVendidoDto?> ObtenerProductoMasVendidoAsync(DateTime inicio, DateTime fin);
    }
}
