using System;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class TrabajadorService : ITrabajadorService
{
    private readonly ITrabajadorRepository _trabajadorRepository;

    public TrabajadorService(ITrabajadorRepository trabajadorRepository) => _trabajadorRepository = trabajadorRepository;

    public async Task<TrabajadorDto> CreateAsync(CrearTrabajadorDto dto)
    {
        string contrasenaHash = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena);

        var trabajador = new Trabajador
        {
            Nombre = dto.Nombre,
            Apellidos = dto.Apellidos,
            Usuario = dto.Usuario,
            ContrasenaHash = contrasenaHash,
            Telefono = dto.Telefono,
            Email = dto.Email
        };
        await _trabajadorRepository.AddAsync(trabajador);

        return ToDto(trabajador);
    }

    
    private TrabajadorDto ToDto(Trabajador entity) => new TrabajadorDto
    {
        TrabajadorId = entity.TrabajadorId,
        Nombre = entity?.Nombre ?? string.Empty,
        Apellidos = entity?.Apellidos ?? string.Empty,
        Usuario = entity?.Usuario ?? string.Empty,
        Telefono = entity?.Telefono ?? string.Empty,
        Email = entity?.Email ?? string.Empty,
        FechaNacimiento = entity?.FechaNacimiento
    };
}
