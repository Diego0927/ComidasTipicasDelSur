using ComidasTipicasDelSur.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Application.Interfaces
{
    public interface ISupervisorService
    {
        Task<IEnumerable<SupervisorDto>> ObtenerTodosAsync();
        Task<SupervisorDto?> ObtenerPorIdAsync(int idsupervisor);
        Task CrearAsync(SupervisorDto cliente);
        Task ActualizarAsync(SupervisorDto cliente);
        Task EliminarAsync(int idsupervisor);
    }
}
