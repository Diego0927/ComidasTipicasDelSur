using ComidasTipicasDelSur.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Application.Interfaces
{
    public interface IMeseroService
    {
        Task<IEnumerable<MeseroDto>> ObtenerTodosAsync();
        Task<MeseroDto?> ObtenerPorIdAsync(int idmesero);
        Task CrearAsync(MeseroDto mesero);
        Task ActualizarAsync(MeseroDto mesero);
        Task EliminarAsync(int idmesero);
    }
}
