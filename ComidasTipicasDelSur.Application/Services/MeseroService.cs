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
    public class MeseroService : IMeseroService
    {
        private readonly IMeseroRepository _meseroRepository;

        public MeseroService(IMeseroRepository meseroRepository)
        {
            _meseroRepository = meseroRepository;
        }

        public async Task<IEnumerable<MeseroDto>> ObtenerTodosAsync()
        {
            var meseros = await _meseroRepository.ObtenerTodosAsync();
            return meseros.Select(m => new MeseroDto
            {
                IdMesero = m.IdMesero,
                Nombres = m.Nombres,
                Apellidos = m.Apellidos,
                Edad = m.Edad,
                Antiguedad = m.Antiguedad
            });
        }

        public async Task<MeseroDto?> ObtenerPorIdAsync(int idMesero)
        {
            var mesero = await _meseroRepository.ObtenerPorIdAsync(idMesero);
            if (mesero == null) return null;

            return new MeseroDto
            {
                IdMesero = mesero.IdMesero,
                Nombres = mesero.Nombres,
                Apellidos = mesero.Apellidos,
                Edad = mesero.Edad,
                Antiguedad = mesero.Antiguedad
            };
        }

        public async Task CrearAsync(MeseroDto meseroDto)
        {
            var mesero = new Mesero
            {
                IdMesero = meseroDto.IdMesero,
                Nombres = meseroDto.Nombres ?? string.Empty,
                Apellidos = meseroDto.Apellidos ?? string.Empty,
                Edad = meseroDto.Edad,
                Antiguedad = meseroDto.Antiguedad
            };

            await _meseroRepository.CrearAsync(mesero);
        }

        public async Task ActualizarAsync(MeseroDto meseroDto)
        {
            var meseroExistente = await _meseroRepository.ObtenerPorIdAsync(meseroDto.IdMesero);
            if (meseroExistente == null)
                throw new KeyNotFoundException("Mesero no encontrado");

            // Actualiza las propiedades del objeto rastreado
            meseroExistente.IdMesero = meseroDto.IdMesero;
            meseroExistente.Nombres = meseroDto.Nombres ?? string.Empty;
            meseroExistente.Apellidos= meseroDto.Apellidos ?? string.Empty;
            meseroExistente.Edad = meseroDto.Edad;
            meseroExistente.Antiguedad = meseroDto.Antiguedad;

            await _meseroRepository.ActualizarAsync(meseroExistente);
        }

        public async Task EliminarAsync(int idMesero)
        {
            var meseroExistente = await _meseroRepository.ObtenerPorIdAsync(idMesero);
            if (meseroExistente == null)
                throw new KeyNotFoundException("Mesero no encontrado");

            await _meseroRepository.EliminarAsync(idMesero);
        }
    }
}