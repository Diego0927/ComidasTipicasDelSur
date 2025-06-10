using ComidasTipicasDelSur.Domain.Entities;
using ComidasTipicasDelSur.Infrastructure.Context;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComidasTipicasDelSur.Infrastructure.Repositories
{
    public class DetalleFacturaRepository : IDetalleFacturaRepository
    {
        private readonly AppDbContext _context;

        public DetalleFacturaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetalleFactura>> ObtenerTodosAsync()
        {
            return await _context.Detalle_Factura.ToListAsync();
        }

        public async Task<DetalleFactura?> ObtenerPorIdAsync(int idDetalleXFactura)
        {
            return await _context.Detalle_Factura.FindAsync(idDetalleXFactura);
        }

        public async Task CrearAsync(DetalleFactura detalleFactura)
        {
            _context.Detalle_Factura.Add(detalleFactura);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(DetalleFactura detalleFactura)
        {
            _context.Detalle_Factura.Update(detalleFactura);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int idDetalleXFactura)
        {
            var detalle = await _context.Detalle_Factura.FindAsync(idDetalleXFactura);
            if (detalle != null)
            {
                _context.Detalle_Factura.Remove(detalle);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<DetalleFactura?> ObtenerFacturasConDetallesAsync(int iddetallexfactura)
        {
            return await _context.Detalle_Factura
                .Include(c => c.Factura)
                .FirstOrDefaultAsync(c => c.IdDetalleXFactura == iddetallexfactura);
        }
    }
}