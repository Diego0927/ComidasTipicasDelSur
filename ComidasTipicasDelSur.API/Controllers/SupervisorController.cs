using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComidasTipicasDelSur.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupervisorController(ISupervisorService supervisorService) : ControllerBase
    {
        private readonly ISupervisorService _supervisorService = supervisorService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var supervisores = await _supervisorService.ObtenerTodosAsync();
            return Ok(supervisores);
        }

        [HttpGet("{idsupervisor:int}")]
        public async Task<IActionResult> GetById(int idsupervisor)
        {
            var supervisor = await _supervisorService.ObtenerPorIdAsync(idsupervisor);
            if (supervisor == null)
                return NotFound();
            return Ok(supervisor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupervisorDto supervisor)
        {
            await _supervisorService.CrearAsync(supervisor);
            return CreatedAtAction(nameof(GetById), new { idsupervisor = supervisor.IdSupervisor }, supervisor);
        }

        [HttpPut("{idsupervisor:int}")]
        public async Task<IActionResult> Update(int idsupervisor, [FromBody] SupervisorDto supervisor)
        {
            if (idsupervisor != supervisor.IdSupervisor)
                return BadRequest("El ID del supervisor no coincide");

            await _supervisorService.ActualizarAsync(supervisor);
            return NoContent();
        }

        [HttpDelete("{idsupervisor:int}")]
        public async Task<IActionResult> Delete(int idsupervisor)
        {
            await _supervisorService.EliminarAsync(idsupervisor);
            return Ok(new { success = true, message = $"Supervisor {idsupervisor} eliminado correctamente" });
        }
    }
}
