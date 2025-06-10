using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComidasTipicasDelSur.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController(IFacturaService facturaService) : ControllerBase
    {
        private readonly IFacturaService _facturaService = facturaService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var facturas = await _facturaService.ObtenerTodosAsync();
            return Ok(facturas);
        }

        [HttpGet("{nrofactura:int}")]
        public async Task<IActionResult> GetById(int nrofactura)
        {
            var factura = await _facturaService.ObtenerPorIdAsync(nrofactura);
            if (factura == null)
                return NotFound();
            return Ok(factura);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FacturaDto factura)
        {
            await _facturaService.CrearAsync(factura);
            return CreatedAtAction(nameof(GetById), new { nrofactura = factura.NroFactura }, factura);
        }

        [HttpPut("{nrofactura:int}")]
        public async Task<IActionResult> Update(int nrofactura, [FromBody] FacturaDto factura)
        {
            if (nrofactura != factura.NroFactura)
                return BadRequest("El número de factura no coincide");

            await _facturaService.ActualizarAsync(factura);
            return NoContent();
        }

        [HttpDelete("{nrofactura:int}")]
        public async Task<IActionResult> Delete(int nrofactura)
        {
            await _facturaService.EliminarAsync(nrofactura);
            return Ok(new { success = true, message = $"Factura {nrofactura} eliminada correctamente" });
        }
    }
}
