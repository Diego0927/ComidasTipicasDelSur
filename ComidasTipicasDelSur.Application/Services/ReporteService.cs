using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Application.Interfaces;
using ComidasTipicasDelSur.Infrastructure.Interfaces;

namespace ComidasTipicasDelSur.Application.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public async Task<IEnumerable<VentaMeseroDto>> ObtenerVentasPorMeseroAsync(DateTime inicio, DateTime fin)
        {
            var ventas = await _reporteRepository.ObtenerVentasPorMeseroAsync(inicio, fin);

            if (!ventas.Any())
                throw new KeyNotFoundException("No se encontraron ventas para los meseros en el rango de fechas especificado.");

            return ventas;
        }

        public async Task<IEnumerable<ClienteTopDto>> ObtenerClientesPorConsumoMinimoAsync(decimal minimo)
        {
            var clientes = await _reporteRepository.ObtenerClientesPorConsumoMinimoAsync(minimo);

            if (!clientes.Any())
                throw new KeyNotFoundException("No se encontraron clientes con consumo igual o superior al mínimo especificado.");

            return clientes;
        }

        public async Task<ProductoMasVendidoDto?> ObtenerProductoMasVendidoAsync(DateTime inicio, DateTime fin)
        {
            var producto = await _reporteRepository.ObtenerProductoMasVendidoAsync(inicio, fin);

            if (producto == null)
                throw new KeyNotFoundException("No se encontró producto vendido en el rango de fechas especificado.");

            return producto;
        }
    }
}
