using ComidasTipicasDelSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<Cliente?> ObtenerPorIdAsync(string identificacion);
        Task CrearAsync(Cliente cliente);
        Task ActualizarAsync(Cliente cliente);
        Task EliminarAsync(string identificacion);
        Task<Cliente?> ObtenerClienteConFacturasAsync(string identificacion);
    }
}