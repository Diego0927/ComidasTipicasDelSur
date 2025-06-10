using ComidasTipicasDelSur.Domain.Entities;
using ComidasTipicasDelSur.Infrastructure.Context;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Repositories
{
    public class MeseroRepository : IMeseroRepository
    {
        private readonly AppDbContext _context;

        public MeseroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Mesero>> ObtenerTodosAsync()
        {
            return await _context.Mesero
                .AsNoTracking()
                .OrderBy(m => m.Apellidos)
                .ThenBy(m => m.Nombres)
                .ToListAsync();
        }

        public async Task<Mesero?> ObtenerPorIdAsync(int idMesero)
        {
            return await _context.Mesero
                .Include(m => m.Facturas)  // Opcional: incluye las facturas asociadas
                .FirstOrDefaultAsync(m => m.IdMesero == idMesero);
        }

        public async Task CrearAsync(Mesero mesero)
        {
            await _context.Mesero.AddAsync(mesero);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Mesero mesero)
        {
            _context.Entry(mesero).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int idMesero)
        {
            var mesero = await _context.Mesero.FindAsync(idMesero);
            if (mesero != null)
            {
                _context.Mesero.Remove(mesero);
                await _context.SaveChangesAsync();
            }
        }

        // Método adicional útil para el negocio
        public async Task<IEnumerable<Mesero>> ObtenerPorAntiguedadAsync(int añosMinimos)
        {
            return await _context.Mesero
                .Where(m => m.Antiguedad >= añosMinimos)
                .OrderByDescending(m => m.Antiguedad)
                .ToListAsync();
        }
    }
}