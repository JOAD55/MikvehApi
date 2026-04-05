using System;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class ClienteService(IClienteRepository clienteRepository) : IClienteService
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    
    public async Task<ClienteDto> CreateAsync(CreateClienteDto dto)
    {
        string? lowerEmail = dto.Email?.ToLower();

        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Apellidos = dto.Apellidos,
            Telefono = dto.Telefono,
            Email = lowerEmail,
            FechaNacimiento = dto.FechaNacimiento
        };

        await _clienteRepository.AddAsync(cliente);

        return ToDto(cliente);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente is null) return false;

        await _clienteRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<ClienteDto>> GetAllAsync()
    {
        var clientes = await _clienteRepository.GetAllAsync();

        return clientes.Select(c => ToDto(c));
    }

    public async Task<ClienteDto?> GetByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        return cliente is null ? null : ToDto(cliente);
    }

    public async Task<ClienteDto?> UpdateAsync(int id, UpdateClienteDto dto)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente is null) return null;

        if (dto.Nombre is not null) cliente.Nombre = dto.Nombre;
        if (dto.Apellidos is not null) cliente.Apellidos = dto.Apellidos;
        if (dto.Telefono is not null) cliente.Telefono = dto.Telefono;
        if (dto.Email is not null) cliente.Email = dto.Email;
        if (dto.FechaNacimiento is not null) cliente.FechaNacimiento = dto.FechaNacimiento;

        await _clienteRepository.UpdateAsync(cliente);

        return ToDto(cliente);
    }

    private ClienteDto ToDto(Cliente entity) => new ClienteDto
    {
        ClienteId = entity.ClienteId,
        Nombre = entity?.Nombre?? string.Empty,
        Apellidos = entity?.Apellidos?? string.Empty,
        Telefono = entity?.Telefono?? string.Empty,
        Email = entity?.Email?? string.Empty,
        FechaNacimiento = entity?.FechaNacimiento
    };
}
