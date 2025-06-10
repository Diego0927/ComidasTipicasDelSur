using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComidasTipicasDelSur.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeseroController(IMeseroService meseroService) : ControllerBase
    {
        private readonly IMeseroService _meseroService = meseroService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meseros = await _meseroService.ObtenerTodosAsync();
            return Ok(meseros);
        }

        [HttpGet("{idmesero:int}")]
        public async Task<IActionResult> GetById(int idmesero)
        {
            var mesero = await _meseroService.ObtenerPorIdAsync(idmesero);
            if (mesero == null)
                return NotFound();
            return Ok(mesero);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MeseroDto mesero)
        {
            await _meseroService.CrearAsync(mesero);
            return CreatedAtAction(nameof(GetById), new { idmesero = mesero.IdMesero }, mesero);
        }

        [HttpPut("{idmesero:int}")]
        public async Task<IActionResult> Update(int idmesero, [FromBody] MeseroDto mesero)
        {
            if (idmesero != mesero.IdMesero)
                return BadRequest("El ID del mesero no coincide");

            await _meseroService.ActualizarAsync(mesero);
            return NoContent();
        }

        [HttpDelete("{idmesero:int}")]
        public async Task<IActionResult> Delete(int idmesero)
        {
            await _meseroService.EliminarAsync(idmesero);
            return Ok(new { success = true, message = $"Mesero {idmesero} eliminado correctamente" });
        }
    }
}
