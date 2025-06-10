using ComidasTipicasDelSur.Shared.DTOs;
using ComidasTipicasDelSur.Application.Interfaces;
using ComidasTipicasDelSur.Domain.Entities;
using ComidasTipicasDelSur.Infrastructure.Interfaces;

namespace ComidasTipicasDelSur.Application.Services
{
    public class SupervisorService : ISupervisorService
    {
        private readonly ISupervisorRepository _supervisorRepository;

        public SupervisorService(ISupervisorRepository supervisorRepository)
        {
            _supervisorRepository = supervisorRepository;
        }

        public async Task<IEnumerable<SupervisorDto>> ObtenerTodosAsync()
        {
            var supervisores = await _supervisorRepository.ObtenerTodosAsync();
            return supervisores.Select(s => new SupervisorDto
            {
                IdSupervisor = s.IdSupervisor,
                Nombres = s.Nombres,
                Apellidos = s.Apellidos,
                Edad = s.Edad,
                Antiguedad = s.Antiguedad
            });
        }

        public async Task<SupervisorDto?> ObtenerPorIdAsync(int idSupervisor)
        {
            var supervisor = await _supervisorRepository.ObtenerPorIdAsync(idSupervisor);
            if (supervisor == null) return null;

            return new SupervisorDto
            {
                IdSupervisor = supervisor.IdSupervisor,
                Nombres = supervisor.Nombres,
                Apellidos = supervisor.Apellidos,
                Edad = supervisor.Edad,
                Antiguedad = supervisor.Antiguedad
            };
        }

        public async Task CrearAsync(SupervisorDto supervisorDto)
        {
            var supervisor = new Supervisor
            {
                IdSupervisor = supervisorDto.IdSupervisor,
                Nombres = supervisorDto.Nombres ?? string.Empty,
                Apellidos = supervisorDto.Apellidos ?? string.Empty,
                Edad = supervisorDto.Edad,
                Antiguedad = supervisorDto.Antiguedad
            };

            await _supervisorRepository.CrearAsync(supervisor);
        }

        public async Task ActualizarAsync(SupervisorDto supervisorDto)
        {
            var supervisorExistente = await _supervisorRepository.ObtenerPorIdAsync(supervisorDto.IdSupervisor);
            if (supervisorExistente == null)
                throw new KeyNotFoundException("Supervisor no encontrado");

            // Actualiza las propiedades del objeto rastreado
            supervisorExistente.IdSupervisor = supervisorDto.IdSupervisor;
            supervisorExistente.Nombres = supervisorDto.Nombres ?? string.Empty;
            supervisorExistente.Apellidos = supervisorDto.Apellidos ?? string.Empty;
            supervisorExistente.Edad = supervisorDto.Edad;
            supervisorExistente.Antiguedad = supervisorDto.Antiguedad;

            await _supervisorRepository.ActualizarAsync(supervisorExistente);
        }

        public async Task EliminarAsync(int idSupervisor)
        {
            var supervisorExistente = await _supervisorRepository.ObtenerPorIdAsync(idSupervisor);
            if (supervisorExistente == null)
                throw new KeyNotFoundException("Supervisor no encontrado");

            await _supervisorRepository.EliminarAsync(idSupervisor);
        }
    }
}