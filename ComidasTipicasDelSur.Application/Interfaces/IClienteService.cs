using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> ObtenerTodosAsync();
        Task<ClienteDto?> ObtenerPorIdAsync(string identificacion);
        Task CrearAsync(ClienteDto cliente);
        Task ActualizarAsync(ClienteDto cliente);
        Task<(bool success, string message)> EliminarAsync(string identificacion);
        Task<Cliente?> ObtenerClienteConFacturasAsync(string identificacion);
    }
}
