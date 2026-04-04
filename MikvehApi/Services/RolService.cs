using System;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class RolService(IRolRepository rolRepository) : IRolService
{
    private readonly IRolRepository _rolRepository = rolRepository;

    public async Task<RolDto> CreateAsync(CrearRolDto dto)
    {
        var rol = new Rol { Nombre = dto.Nombre, Descripcion = dto.Descripcion };

        await _rolRepository.AddAsync(rol);
        return ToDto(rol);
    }

    public async Task<IEnumerable<RolDto>> GetAllAsync()
    {
        var roles = await _rolRepository.GetAllAsync();

        return roles.Select(r => ToDto(r));
    }

    public async Task<RolDto?> GetByIdAsync(int id)
    {
        var rol = await _rolRepository.GetByIdAsync(id);

        if (rol is null) return null;

        return ToDto(rol);
    }

    public async Task<RolDto?> UpdateAsync(int id, ActualizarRolDto dto)
    {
        var rol = await _rolRepository.GetByIdAsync(id);

        if (rol is null) return null;

        if (dto.Nombre is not null) rol.Nombre = dto.Nombre;
        if (dto.Descripcion is not null) rol.Descripcion = dto.Descripcion;

        await _rolRepository.UpdateAsync(rol);

        return ToDto(rol);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rol = await _rolRepository.GetByIdAsync(id);

        if (rol is null) return false;

        await _rolRepository.DeleteAsync(id);
        return true;
    }

    private RolDto ToDto(Rol entity) => new RolDto
    {
        RolId = entity.RolId,
        Nombre = entity.Nombre,
        Descripcion = entity.Descripcion ?? string.Empty
    };
}
