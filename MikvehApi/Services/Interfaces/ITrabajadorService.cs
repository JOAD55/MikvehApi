using System;
using MikvehApi.DTOs;

namespace MikvehApi.Services.Interfaces;

public interface ITrabajadorService
{
    Task<TrabajadorDto> CreateAsync(CrearTrabajadorDto dto);
}
