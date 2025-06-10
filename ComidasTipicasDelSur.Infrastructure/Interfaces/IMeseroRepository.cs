using ComidasTipicasDelSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Interfaces
{
    public interface IMeseroRepository
    {
        Task<IEnumerable<Mesero>> ObtenerTodosAsync();
        Task<Mesero?> ObtenerPorIdAsync(int idmesero);
        Task CrearAsync(Mesero mesero);
        Task ActualizarAsync(Mesero mesero);
        Task EliminarAsync(int idmesero);
    }
}
