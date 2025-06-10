using ComidasTipicasDelSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Interfaces
{
    public interface ISupervisorRepository
    {
        Task<IEnumerable<Supervisor>> ObtenerTodosAsync();
        Task<Supervisor?> ObtenerPorIdAsync(int idsupervisor);
        Task CrearAsync(Supervisor cliente);
        Task ActualizarAsync(Supervisor cliente);
        Task EliminarAsync(int idsupervisor);
    }
}