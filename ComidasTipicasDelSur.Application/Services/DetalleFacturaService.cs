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
    public class DetalleFacturaService : IDetalleFacturaService
    {
        private readonly IDetalleFacturaRepository _detalleFacturaRepository;

        public DetalleFacturaService(IDetalleFacturaRepository detalleFacturaRepository)
        {
            _detalleFacturaRepository = detalleFacturaRepository;
        }

        public async Task<IEnumerable<DetalleFacturaDto>> ObtenerTodosAsync()
        {
            var detalles = await _detalleFacturaRepository.ObtenerTodosAsync();
            return detalles.Select(d => new DetalleFacturaDto
            {
                IdDetalleXFactura = d.IdDetalleXFactura,
                NroFactura = d.NroFactura,
                IdSupervisor = d.IdSupervisor,
                Plato = d.Plato,
                Valor = d.Valor
            });
        }

        public async Task<DetalleFacturaDto?> ObtenerPorIdAsync(int idDetalleXFactura)
        {
            var detalle = await _detalleFacturaRepository.ObtenerPorIdAsync(idDetalleXFactura);
            if (detalle == null) return null;

            return new DetalleFacturaDto
            {
                IdDetalleXFactura = detalle.IdDetalleXFactura,
                NroFactura = detalle.NroFactura,
                IdSupervisor = detalle.IdSupervisor,
                Plato = detalle.Plato,
                Valor = detalle.Valor
            };
        }

        public async Task CrearAsync(DetalleFacturaDto detalleFacturaDto)
        {
            var detalleFactura = new DetalleFactura
            {
                IdDetalleXFactura = detalleFacturaDto.IdDetalleXFactura,
                NroFactura = detalleFacturaDto.NroFactura,
                IdSupervisor = detalleFacturaDto.IdSupervisor,
                Plato = detalleFacturaDto.Plato,
                Valor = detalleFacturaDto.Valor
            };

            await _detalleFacturaRepository.CrearAsync(detalleFactura);
        }

        public async Task ActualizarAsync(DetalleFacturaDto detalleFacturaDto)
        {
            var detalleExistente = await _detalleFacturaRepository.ObtenerPorIdAsync(detalleFacturaDto.IdDetalleXFactura);
            if (detalleExistente == null)
                throw new KeyNotFoundException("Detalle de factura no encontrado");

            var detalleActualizado = new DetalleFactura
            {
                IdDetalleXFactura = detalleFacturaDto.IdDetalleXFactura,
                NroFactura = detalleFacturaDto.NroFactura,
                IdSupervisor = detalleFacturaDto.IdSupervisor,
                Plato = detalleFacturaDto.Plato,
                Valor = detalleFacturaDto.Valor
            };

            await _detalleFacturaRepository.ActualizarAsync(detalleActualizado);
        }

        public async Task<(bool success, string message)> EliminarAsync(int iddetallexfactura)
        {
            var detallefactura = await _detalleFacturaRepository.ObtenerPorIdAsync(iddetallexfactura);

            if (detallefactura == null)
                return (false, "Detalle no encontrado");

            await _detalleFacturaRepository.EliminarAsync(iddetallexfactura);
            return (true, "Detalle eliminado correctamente");
        }

    }
}