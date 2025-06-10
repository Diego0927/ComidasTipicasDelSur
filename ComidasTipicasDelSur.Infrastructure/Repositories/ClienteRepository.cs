using ComidasTipicasDelSur.Domain.Entities;
using ComidasTipicasDelSur.Infrastructure.Context;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComidasTipicasDelSur.Infrastructure.Repositories
{
    public class ClienteRepository(AppDbContext context) : IClienteRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            return await _context.Cliente.ToListAsync();
        }

        public async Task<Cliente?> ObtenerPorIdAsync(string identificacion)
        {
            return await _context.Cliente.FindAsync(identificacion);
        }

        public async Task CrearAsync(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Cliente cliente)
        {
            // No se necesita _context.Update(cliente) si el objeto ya está rastreado
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(string identificacion)
        {
            var cliente = await _context.Cliente.FindAsync(identificacion);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Cliente?> ObtenerClienteConFacturasAsync(string identificacion)
        {
            return await _context.Cliente
                .Include(c => c.Facturas)
                .FirstOrDefaultAsync(c => c.Identificacion == identificacion);
        }
    }
}
