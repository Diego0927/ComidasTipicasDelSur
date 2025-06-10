using Microsoft.AspNetCore.Mvc;
using ComidasTipicasDelSur.Application.Interfaces;

namespace ComidasTipicasDelSur.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("ventas-por-mesero")]
        public async Task<IActionResult> GetVentasMesero([FromQuery] DateTime inicio, [FromQuery] DateTime fin)
        {
            var resultado = await _reporteService.ObtenerVentasPorMeseroAsync(inicio, fin);
            return Ok(resultado);
        }

        [HttpGet("clientes-top")]
        public async Task<IActionResult> GetClientesTop([FromQuery] decimal minimo)
        {
            var resultado = await _reporteService.ObtenerClientesPorConsumoMinimoAsync(minimo);
            return Ok(resultado);
        }

        [HttpGet("producto-mas-vendido")]
        public async Task<IActionResult> GetProductoMasVendido([FromQuery] DateTime inicio, [FromQuery] DateTime fin)
        {
            var resultado = await _reporteService.ObtenerProductoMasVendidoAsync(inicio, fin);
            if (resultado == null) return NotFound("No se encontró información para ese periodo");
            return Ok(resultado);
        }
    }
}
