using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Application.Interfaces;
using ComidasTipicasDelSur.Domain.Entities;
using ComidasTipicasDelSur.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComidasTipicasDelSur.Application.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<IEnumerable<FacturaDto>> ObtenerTodosAsync()
        {
            var facturas = await _facturaRepository.ObtenerTodosAsync();
            return facturas.Select(f => new FacturaDto
            {
                NroFactura = f.NroFactura,
                IdCliente = f.IdCliente,
                NroMesa = f.NroMesa,
                IdMesero = f.IdMesero,
                Fecha = f.Fecha
            });
        }

        public async Task<FacturaDto?> ObtenerPorIdAsync(int nroFactura)
        {
            var factura = await _facturaRepository.ObtenerPorIdAsync(nroFactura);
            if (factura == null) return null;

            return new FacturaDto
            {
                NroFactura = factura.NroFactura,
                IdCliente = factura.IdCliente,
                NroMesa = factura.NroMesa,
                IdMesero = factura.IdMesero,
                Fecha = factura.Fecha
            };
        }

        public async Task CrearAsync(FacturaDto facturaDto)
        {
            var factura = new Factura
            {
                NroFactura = facturaDto.NroFactura,
                IdCliente = facturaDto.IdCliente ?? string.Empty,
                NroMesa = facturaDto.NroMesa,
                IdMesero = facturaDto.IdMesero,
                Fecha = facturaDto.Fecha
            };

            await _facturaRepository.CrearAsync(factura);
        }

        public async Task ActualizarAsync(FacturaDto facturaDto)
        {
            var facturaExistente = await _facturaRepository.ObtenerPorIdAsync(facturaDto.NroFactura);
            if (facturaExistente == null)
                throw new KeyNotFoundException("Factura no encontrada");

            var facturaActualizada = new Factura
            {
                NroFactura = facturaDto.NroFactura,
                IdCliente = facturaDto.IdCliente ?? string.Empty,
                NroMesa = facturaDto.NroMesa,
                IdMesero = facturaDto.IdMesero,
                Fecha = facturaDto.Fecha
            };

            await _facturaRepository.ActualizarAsync(facturaActualizada);
        }

        public async Task EliminarAsync(int nroFactura)
        {
            var facturaExistente = await _facturaRepository.ObtenerPorIdAsync(nroFactura);
            if (facturaExistente == null)
                throw new KeyNotFoundException("Factura no encontrada");

            await _facturaRepository.EliminarAsync(nroFactura);
        }
    }
}