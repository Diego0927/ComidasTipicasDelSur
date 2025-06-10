using ComidasTipicasDelSur.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Interfaces
{
    public interface IMesaRepository
    {
        Task<IEnumerable<Mesa>> ObtenerTodosAsync();
        Task<Mesa?> ObtenerPorIdAsync(int nromesa);
        Task CrearAsync(Mesa mesa);
        Task ActualizarAsync(Mesa mesa);
        Task EliminarAsync(int nromesa);
    }
}
