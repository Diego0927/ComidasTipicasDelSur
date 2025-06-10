using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComidasTipicasDelSur.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MesaController(IMesaService mesaService) : ControllerBase
    {
        private readonly IMesaService _mesaService = mesaService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var mesas = await _mesaService.ObtenerTodosAsync();
            return Ok(mesas);
        }

        [HttpGet("{nromesa:int}")]
        public async Task<IActionResult> GetById(int nromesa)
        {
            var mesa = await _mesaService.ObtenerPorIdAsync(nromesa);
            if (mesa == null)
                return NotFound();
            return Ok(mesa);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MesaDto mesa)
        {
            await _mesaService.CrearAsync(mesa);
            return CreatedAtAction(nameof(GetById), new { nromesa = mesa.NroMesa }, mesa);
        }

        [HttpPut("{nromesa:int}")]
        public async Task<IActionResult> Update(int nromesa, [FromBody] MesaDto mesa)
        {
            if (nromesa != mesa.NroMesa)
                return BadRequest("El número de mesa no coincide");

            await _mesaService.ActualizarAsync(mesa);
            return NoContent();
        }

        [HttpDelete("{nromesa:int}")]
        public async Task<IActionResult> Delete(int nromesa)
        {
            await _mesaService.EliminarAsync(nromesa);
            return Ok(new { success = true, message = $"Mesa {nromesa} eliminada correctamente" });
        }
    }
}
