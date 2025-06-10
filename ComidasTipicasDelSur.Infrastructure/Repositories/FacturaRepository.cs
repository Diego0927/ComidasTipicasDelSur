using ComidasTipicasDelSur.Domain.Entities;
using ComidasTipicasDelSur.Infrastructure.Context;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Infrastructure.Repositories
{
    public class FacturaRepository(AppDbContext context) : IFacturaRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Factura>> ObtenerTodosAsync()
        {
            return await _context.Factura
                .Include(f => f.Cliente)
                .Include(f => f.Mesero)
                .Include(f => f.Mesa)
                .Include(f => f.Detalles)
                .OrderByDescending(f => f.Fecha)
                .ThenBy(f => f.NroFactura)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Factura?> ObtenerPorIdAsync(int nroFactura)
        {
            return await _context.Factura
                .Include(f => f.Cliente)
                .Include(f => f.Mesero)
                .Include(f => f.Mesa)
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.NroFactura == nroFactura);
        }

        public async Task CrearAsync(Factura factura)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Verificar existencia de relaciones
                await ValidarRelacionesFactura(factura);

                await _context.Factura.AddAsync(factura);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task ActualizarAsync(Factura factura)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Verificar existencia de relaciones
                await ValidarRelacionesFactura(factura);

                _context.Factura.Update(factura);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task EliminarAsync(int nroFactura)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var factura = await _context.Factura
                    .Include(f => f.Detalles)
                    .FirstOrDefaultAsync(f => f.NroFactura == nroFactura);

                if (factura == null)
                    throw new KeyNotFoundException("Factura no encontrada");

                if (factura.Detalles.Any())
                    throw new InvalidOperationException("No se puede eliminar una factura con detalles asociados");

                _context.Factura.Remove(factura);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task ValidarRelacionesFactura(Factura factura)
        {
            // Verificar cliente
            var clienteExiste = await _context.Cliente
                .CountAsync(c => c.Identificacion == factura.IdCliente) > 0;
            if (!clienteExiste)
                throw new InvalidOperationException("El cliente especificado no existe");

            // Verificar mesa
            var mesaExiste = await _context.Mesa
                .CountAsync(m => m.NroMesa == factura.NroMesa) > 0;
            if (!mesaExiste)
                throw new InvalidOperationException("La mesa especificada no existe");

            // Verificar mesero
            var meseroExiste = await _context.Mesero
                .CountAsync(m => m.IdMesero == factura.IdMesero) > 0;
            if (!meseroExiste)
                throw new InvalidOperationException("El mesero especificado no existe");
        }

        // Métodos adicionales útiles para el negocio
        public async Task<IEnumerable<Factura>> ObtenerPorFechaAsync(DateTime fecha)
        {
            return await _context.Factura
                .Where(f => f.Fecha.Date == fecha.Date)
                .Include(f => f.Cliente)
                .Include(f => f.Mesero)
                .OrderBy(f => f.NroFactura)
                .ToListAsync();
        }

        public async Task<IEnumerable<Factura>> ObtenerPorMeseroAsync(int idMesero)
        {
            return await _context.Factura
                .Where(f => f.IdMesero == idMesero)
                .Include(f => f.Cliente)
                .Include(f => f.Mesa)
                .OrderByDescending(f => f.Fecha)
                .ToListAsync();
        }
    }
}