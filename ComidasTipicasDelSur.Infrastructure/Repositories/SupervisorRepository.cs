using ComidasTipicasDelSur.Domain.Entities;
using ComidasTipicasDelSur.Infrastructure.Context;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComidasTipicasDelSur.Infrastructure.Repositories
{
    public class SupervisorRepository : ISupervisorRepository
    {
        private readonly AppDbContext _context;

        public SupervisorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supervisor>> ObtenerTodosAsync()
        {
            return await _context.Supervisor
                .AsNoTracking()
                .OrderBy(s => s.Apellidos)
                .ThenBy(s => s.Nombres)
                .ToListAsync();
        }

        public async Task<Supervisor?> ObtenerPorIdAsync(int idSupervisor)
        {
            return await _context.Supervisor
                .Include(s => s.DetallesFactura)  // Incluye los detalles de factura asociados
                .FirstOrDefaultAsync(s => s.IdSupervisor == idSupervisor);
        }

        public async Task CrearAsync(Supervisor supervisor)
        {
            await _context.Supervisor.AddAsync(supervisor);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Supervisor supervisor)
        {
            _context.Entry(supervisor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int idSupervisor)
        {
            var supervisor = await _context.Supervisor
                .Include(s => s.DetallesFactura)
                .FirstOrDefaultAsync(s => s.IdSupervisor == idSupervisor);

            if (supervisor != null)
            {
                // Verificar si tiene detalles de factura asociados
                if (supervisor.DetallesFactura.Any())
                {
                    throw new InvalidOperationException(
                        "No se puede eliminar el supervisor porque tiene detalles de factura asociados");
                }

                _context.Supervisor.Remove(supervisor);
                await _context.SaveChangesAsync();
            }
        }

        // Método adicional para obtener supervisores por antigüedad mínima
        public async Task<IEnumerable<Supervisor>> ObtenerPorAntiguedadAsync(int añosMinimos)
        {
            return await _context.Supervisor
                .Where(s => s.Antiguedad >= añosMinimos)
                .OrderByDescending(s => s.Antiguedad)
                .ToListAsync();
        }
    }
}