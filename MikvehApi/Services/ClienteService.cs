using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class ClienteService(IClienteRepository clienteRepository, IMapper mapper) : IClienteService
{
    private readonly IClienteRepository _clienteRepository = clienteRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ClienteDto> CreateAsync(CreateClienteDto dto)
    {
        var cliente = _mapper.Map<Cliente>(dto);
        cliente.Email = cliente.Email?.ToLower();

        await _clienteRepository.AddAsync(cliente);

        return _mapper.Map<ClienteDto>(cliente);
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

        return clientes.Select(c => _mapper.Map<ClienteDto>(c));
    }

    public async Task<ClienteDto?> GetByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        return cliente is null ? null : _mapper.Map<ClienteDto>(cliente);
    }

    public async Task<ClienteDto?> UpdateAsync(int id, UpdateClienteDto dto)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);

        if (cliente is null) return null;

        if (dto.Nombre is not null) cliente.Nombre = dto.Nombre;
        if (dto.Apellidos is not null) cliente.Apellidos = dto.Apellidos;
        if (dto.Telefono is not null) cliente.Telefono = dto.Telefono;
        if (dto.Email is not null) cliente.Email = dto.Email.ToLower();
        if (dto.FechaNacimiento is not null) cliente.FechaNacimiento = dto.FechaNacimiento;

        await _clienteRepository.UpdateAsync(cliente);

        return _mapper.Map<ClienteDto>(cliente);
    }
}
