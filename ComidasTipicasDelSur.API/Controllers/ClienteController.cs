using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComidasTipicasDelSur.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController(IClienteService clienteService) : ControllerBase
    {
        private readonly IClienteService _clienteService = clienteService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);
            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDto cliente)
        {
            await _clienteService.CrearAsync(cliente);
            return CreatedAtAction(nameof(GetById), new { id = cliente.Identificacion }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ClienteDto cliente)
        {
            if (id != cliente.Identificacion)
                return BadRequest("El ID no coincide");

            await _clienteService.ActualizarAsync(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var (success, message) = await _clienteService.EliminarAsync(id);

            if (!success)
            {
                return BadRequest(new
                {
                    success = false,
                    message,
                    actionRequired = "Eliminar primero las facturas asociadas o cambiar su asignación"
                });
            }

            return Ok(new { success = true, message = message });
        }
    }
}
