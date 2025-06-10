using ComidasTipicasDelSur.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Application.Interfaces
{
    public interface IMesaService
    {
        Task<IEnumerable<MesaDto>> ObtenerTodosAsync();
        Task<MesaDto?> ObtenerPorIdAsync(int nromesa);
        Task CrearAsync(MesaDto mesa);
        Task ActualizarAsync(MesaDto mesa);
        Task EliminarAsync(int nromesa);
    }
}
