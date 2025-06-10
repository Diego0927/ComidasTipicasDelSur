using ComidasTipicasDelSur.Domain.Entities;
using ComidasTipicasDelSur.Infrastructure.Context;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Repositories
{
    public class MesaRepository : IMesaRepository
    {
        private readonly AppDbContext _context;

        public MesaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mesa>> ObtenerTodosAsync()
        {
            return await _context.Mesa
                .AsNoTracking()  // Mejora rendimiento para operaciones de solo lectura
                .OrderBy(m => m.NroMesa)  // Ordena por número de mesa
                .ToListAsync();
        }

        public async Task<Mesa?> ObtenerPorIdAsync(int nroMesa)
        {
            return await _context.Mesa
                .FirstOrDefaultAsync(m => m.NroMesa == nroMesa);
        }

        public async Task CrearAsync(Mesa mesa)
        {
            await _context.Mesa.AddAsync(mesa);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Mesa mesa)
        {
            _context.Entry(mesa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int nroMesa)
        {
            var mesa = await _context.Mesa.FindAsync(nroMesa);
            if (mesa != null)
            {
                _context.Mesa.Remove(mesa);
                await _context.SaveChangesAsync();
            }
        }

        // Método adicional útil para el negocio
        public async Task<IEnumerable<Mesa>> ObtenerMesasDisponiblesAsync()
        {
            return await _context.Mesa
                .Where(m => m.Reservada == 'N')  // Filtra mesas no reservadas
                .OrderBy(m => m.NroMesa)
                .ToListAsync();
        }
    }
}