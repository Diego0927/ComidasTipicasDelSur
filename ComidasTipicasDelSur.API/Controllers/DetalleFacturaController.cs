using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComidasTipicasDelSur.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleFacturaController(IDetalleFacturaService detalleFacturaService) : ControllerBase
    {
        private readonly IDetalleFacturaService _detalleFacturaService = detalleFacturaService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var detalles = await _detalleFacturaService.ObtenerTodosAsync();
            return Ok(detalles);
        }

        [HttpGet("{iddetallexfactura:int}")]
        public async Task<IActionResult> GetById(int iddetallexfactura)
        {
            var detalle = await _detalleFacturaService.ObtenerPorIdAsync(iddetallexfactura);
            if (detalle == null)
                return NotFound();
            return Ok(detalle);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DetalleFacturaDto detalleFactura)
        {
            await _detalleFacturaService.CrearAsync(detalleFactura);
            return CreatedAtAction(nameof(GetById), new { iddetallexfactura = detalleFactura.IdDetalleXFactura }, detalleFactura);
        }

        [HttpPut("{iddetallexfactura:int}")]
        public async Task<IActionResult> Update(int iddetallexfactura, [FromBody] DetalleFacturaDto detalleFactura)
        {
            if (iddetallexfactura != detalleFactura.IdDetalleXFactura)
                return BadRequest("El ID del detalle no coincide");

            await _detalleFacturaService.ActualizarAsync(detalleFactura);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var (success, message) = await _detalleFacturaService.EliminarAsync(id);

            if (!success)
            {
                return NotFound(new
                {
                    success = false,
                    message
                });
            }

            return Ok(new
            {
                success = true,
                message
            });
        }

    }
}
