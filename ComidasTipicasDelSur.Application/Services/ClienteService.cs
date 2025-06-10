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
    public class ClienteService(IClienteRepository clienteRepository) : IClienteService
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;

        public async Task<IEnumerable<ClienteDto>> ObtenerTodosAsync()
        {
            var clientes = await _clienteRepository.ObtenerTodosAsync();
            return clientes.Select(c => new ClienteDto
            {
                Identificacion = c.Identificacion ?? string.Empty,
                Nombre = c.Nombre ?? string.Empty,
                Apellidos = c.Apellidos ?? string.Empty,
                Direccion = c.Direccion ?? string.Empty,
                Telefono = c.Telefono ?? string.Empty
            });
        }

        public async Task<ClienteDto?> ObtenerPorIdAsync(string identificacion)
        {
            var cliente = await _clienteRepository.ObtenerPorIdAsync(identificacion);
            if (cliente == null) return null;

            return new ClienteDto
            {
                Identificacion = cliente.Identificacion ?? string.Empty,
                Nombre = cliente.Nombre ?? string.Empty,
                Apellidos = cliente.Apellidos ?? string.Empty,
                Direccion = cliente.Direccion ?? string.Empty,
                Telefono = cliente.Telefono ?? string.Empty
            };
        }

        public async Task CrearAsync(ClienteDto clienteDto)
        {
            var cliente = new Cliente
            {
                Identificacion = clienteDto.Identificacion ?? string.Empty,
                Nombre = clienteDto.Nombre ?? string.Empty,
                Apellidos = clienteDto.Apellidos ?? string.Empty,
                Direccion = clienteDto.Direccion ?? string.Empty,
                Telefono = clienteDto.Telefono ?? string.Empty
            };

            await _clienteRepository.CrearAsync(cliente);
        }

        public async Task ActualizarAsync(ClienteDto clienteDto)
        {
            var clienteExistente = await _clienteRepository.ObtenerPorIdAsync(clienteDto.Identificacion ?? string.Empty);
            if (clienteExistente == null)
                throw new KeyNotFoundException("Cliente no encontrado");

            // Actualiza las propiedades del objeto rastreado
            clienteExistente.Nombre = clienteDto.Nombre ?? string.Empty;
            clienteExistente.Apellidos = clienteDto.Apellidos ?? string.Empty;
            clienteExistente.Direccion = clienteDto.Direccion ?? string.Empty;
            clienteExistente.Telefono = clienteDto.Telefono ?? string.Empty;

            await _clienteRepository.ActualizarAsync(clienteExistente); // Pasa el objeto rastreado
        }

        public async Task<(bool success, string message)> EliminarAsync(string identificacion)
        {
            var cliente = await _clienteRepository.ObtenerClienteConFacturasAsync(identificacion);

            if (cliente == null)
                return (false, "Cliente no encontrado");

            if (cliente.Facturas != null && cliente.Facturas.Count != 0)
            {
                return (false, $"No se puede eliminar el cliente porque tiene {cliente.Facturas.Count} factura(s) asociada(s). " +
                              "Primero debe eliminar las facturas relacionadas o cambiar su asignación.");
            }

            await _clienteRepository.EliminarAsync(identificacion);
            return (true, "Cliente eliminado correctamente");
        }

        public async Task<Cliente?> ObtenerClienteConFacturasAsync(string identificacion)
        {
            return await _clienteRepository.ObtenerClienteConFacturasAsync(identificacion);
        }
    }
}