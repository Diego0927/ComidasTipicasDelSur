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
    public class MesaService : IMesaService
    {
        private readonly IMesaRepository _mesaRepository;

        public MesaService(IMesaRepository mesaRepository)
        {
            _mesaRepository = mesaRepository;
        }

        public async Task<IEnumerable<MesaDto>> ObtenerTodosAsync()
        {
            var mesas = await _mesaRepository.ObtenerTodosAsync();
            return mesas.Select(m => new MesaDto
            {
                NroMesa = m.NroMesa,
                Nombre = m.Nombre,
                Reservada = m.Reservada,
                Puestos = m.Puestos
            });
        }

        public async Task<MesaDto?> ObtenerPorIdAsync(int nroMesa)
        {
            var mesa = await _mesaRepository.ObtenerPorIdAsync(nroMesa);
            if (mesa == null) return null;

            return new MesaDto
            {
                NroMesa = mesa.NroMesa,
                Nombre = mesa.Nombre,
                Reservada = mesa.Reservada,
                Puestos = mesa.Puestos
            };
        }

        public async Task CrearAsync(MesaDto mesaDto)
        {
            var mesa = new Mesa
            {
                NroMesa = mesaDto.NroMesa,
                Nombre = mesaDto.Nombre,
                Reservada = mesaDto.Reservada,
                Puestos = mesaDto.Puestos
            };

            await _mesaRepository.CrearAsync(mesa);
        }

        public async Task ActualizarAsync(MesaDto mesaDto)
        {
            var mesaExistente = await _mesaRepository.ObtenerPorIdAsync(mesaDto.NroMesa);
            if (mesaExistente == null)
                throw new KeyNotFoundException("Mesa no encontrada");

            // Actualiza las propiedades del objeto rastreado
            mesaExistente.NroMesa = mesaDto.NroMesa;
            mesaExistente.Nombre = mesaDto.Nombre ?? string.Empty;
            mesaExistente.Reservada = mesaDto.Reservada;
            mesaExistente.Puestos = mesaDto.Puestos;

            await _mesaRepository.ActualizarAsync(mesaExistente);
        }


        public async Task EliminarAsync(int nroMesa)
        {
            var mesaExistente = await _mesaRepository.ObtenerPorIdAsync(nroMesa);
            if (mesaExistente == null)
                throw new KeyNotFoundException("Mesa no encontrada");

            await _mesaRepository.EliminarAsync(nroMesa);
        }
    }
}