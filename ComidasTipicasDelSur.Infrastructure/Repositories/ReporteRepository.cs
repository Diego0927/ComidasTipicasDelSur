using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Infrastructure.Context;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComidasTipicasDelSur.Infrastructure.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly AppDbContext _context;

        public ReporteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VentaMeseroDto>> ObtenerVentasPorMeseroAsync(DateTime inicio, DateTime fin)
        {
            var resultado = await (from m in _context.Mesero
                                   join f in _context.Factura on m.IdMesero equals f.IdMesero into ventas
                                   from f in ventas.DefaultIfEmpty()
                                   join d in _context.Detalle_Factura on f.NroFactura equals d.NroFactura into detalles
                                   from d in detalles.DefaultIfEmpty()
                                   where f == null || (f.Fecha >= inicio && f.Fecha <= fin)
                                   group d by new { m.IdMesero, m.Nombres, m.Apellidos } into g
                                   select new VentaMeseroDto
                                   {
                                       IdMesero = g.Key.IdMesero,
                                       Nombres = g.Key.Nombres,
                                       Apellidos = g.Key.Apellidos,
                                       TotalVendido = g.Sum(x => x != null ? x.Valor : 0)
                                   }).ToListAsync();

            return resultado;
        }

        public async Task<IEnumerable<ClienteTopDto>> ObtenerClientesPorConsumoMinimoAsync(decimal minimo)
        {
            var datos = await (from c in _context.Cliente
                               join f in _context.Factura on c.Identificacion equals f.IdCliente
                               join d in _context.Detalle_Factura on f.NroFactura equals d.NroFactura
                               select new
                               {
                                   c.Identificacion,
                                   NombreCompleto = c.Nombre + " " + c.Apellidos,
                                   d.Valor
                               }).ToListAsync(); // Aquí EF puede traducir todo a SQL

            // Agrupar en memoria
            var resultado = datos
                .GroupBy(x => new { x.Identificacion, x.NombreCompleto })
                .Select(g => new ClienteTopDto
                {
                    Identificacion = g.Key.Identificacion,
                    NombreCompleto = g.Key.NombreCompleto,
                    TotalConsumo = g.Sum(x => x.Valor)
                })
                .Where(x => x.TotalConsumo >= minimo)
                .ToList();

            return resultado;
        }

        public async Task<ProductoMasVendidoDto?> ObtenerProductoMasVendidoAsync(DateTime inicio, DateTime fin)
        {
            var resultado = await (from f in _context.Factura
                                   join d in _context.Detalle_Factura on f.NroFactura equals d.NroFactura
                                   where f.Fecha >= inicio && f.Fecha <= fin
                                   group d by d.Plato into g
                                   orderby g.Sum(x => x.Valor) descending
                                   select new ProductoMasVendidoDto
                                   {
                                       Plato = g.Key,
                                       CantidadVendida = g.Count(),
                                       MontoTotal = g.Sum(x => x.Valor)
                                   }).FirstOrDefaultAsync();

            return resultado;
        }
    }
}
