using System;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class TrabajadorService(ITrabajadorRepository trabajadorRepository) : ITrabajadorService
{
    private readonly ITrabajadorRepository _trabajadorRepository = trabajadorRepository;

    public async Task<IEnumerable<TrabajadorDto>> GetAllAsync()
    {
        var trabajadores = await _trabajadorRepository.GetAllAsync();

        return trabajadores.Select(t => ToDto(t));
    }

    public async Task<TrabajadorDto?> GetByIdAsync(int id)
    {
        var trabajador = await _trabajadorRepository.GetByIdAsync(id);

        if (trabajador is null) return null;

        return ToDto(trabajador);
    }

    public async Task<TrabajadorDto?> GetByUserAsync(string user)
    {
        var trabajador = await _trabajadorRepository.GetByUserAsync(user);

        if (trabajador is null) return null;

        return ToDto(trabajador);
    }

    public async Task<TrabajadorDto> CreateAsync(CreateTrabajadorDto dto)
    {
        string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena);
        string lowerUser = dto.Usuario.ToLower();
        string? lowerEmail = dto.Email?.ToLower();

        var trabajador = new Trabajador
        {
            Nombre = dto.Nombre,
            Apellidos = dto.Apellidos,
            Usuario = lowerUser,
            ContrasenaHash = contrasenaHash,
            Telefono = dto.Telefono,
            Email = lowerEmail,
            FechaNacimiento = dto.FechaNacimiento,
            RolId = dto.RolId
        };

        await _trabajadorRepository.AddAsync(trabajador);

        var trabajadorConRol = await _trabajadorRepository.GetByIdAsync(trabajador.TrabajadorId);
        return ToDto(trabajadorConRol!);
    }

    public async Task<TrabajadorDto?> UpdateAsync(int id, UpdateTrabajadorDto dto)
    {
        var trabajador = await _trabajadorRepository.GetByIdAsync(id);

        if (trabajador is null) return null;

        if (dto.Nombre is not null) trabajador.Nombre = dto.Nombre;
        if (dto.Apellidos is not null) trabajador.Apellidos = dto.Apellidos;
        if (dto.Telefono is not null) trabajador.Telefono = dto.Telefono;
        if (dto.Email is not null) trabajador.Email = dto.Email;
        if (dto.FechaNacimiento is not null) trabajador.FechaNacimiento = dto.FechaNacimiento;
        if (dto.RolId is not null) trabajador.RolId = (int)dto.RolId;

        await _trabajadorRepository.UpdateAsync(trabajador);

        return ToDto(trabajador);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var trabajador = await _trabajadorRepository.GetByIdAsync(id);

        if (trabajador is null) return false;

        await _trabajadorRepository.DeleteAsync(id);
        return true;
    }



    private TrabajadorDto ToDto(Trabajador entity) => new TrabajadorDto
    {
        TrabajadorId = entity.TrabajadorId,
        Nombre = entity.Nombre ?? string.Empty,
        Apellidos = entity.Apellidos ?? string.Empty,
        Usuario = entity.Usuario ?? string.Empty,
        Telefono = entity.Telefono ?? string.Empty,
        Email = entity.Email ?? string.Empty,
        FechaNacimiento = entity.FechaNacimiento,
        RolId = entity.RolId,
        RolNombre = entity.Rol?.Nombre
    };

}
