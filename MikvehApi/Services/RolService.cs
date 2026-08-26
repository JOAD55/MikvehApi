using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class RolService(IRolRepository rolRepository, IMapper mapper) : IRolService
{
    private readonly IRolRepository _rolRepository = rolRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<RolDto> CreateAsync(CreateRolDto dto)
    {
        var rol = _mapper.Map<Rol>(dto);

        await _rolRepository.AddAsync(rol);
        return _mapper.Map<RolDto>(rol);
    }

    public async Task<IEnumerable<RolDto>> GetAllAsync()
    {
        var roles = await _rolRepository.GetAllAsync();

        return roles.Select(r => _mapper.Map<RolDto>(r));
    }

    public async Task<RolDto?> GetByIdAsync(int id)
    {
        var rol = await _rolRepository.GetByIdAsync(id);

        if (rol is null) return null;

        return _mapper.Map<RolDto>(rol);
    }

    public async Task<RolDto?> UpdateAsync(int id, UpdateRolDto dto)
    {
        var rol = await _rolRepository.GetByIdAsync(id);

        if (rol is null) return null;

        if (dto.Nombre is not null) rol.Nombre = dto.Nombre;
        if (dto.Descripcion is not null) rol.Descripcion = dto.Descripcion;

        await _rolRepository.UpdateAsync(rol);

        return _mapper.Map<RolDto>(rol);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rol = await _rolRepository.GetByIdAsync(id);

        if (rol is null) return false;

        await _rolRepository.DeleteAsync(id);
        return true;
    }
}
