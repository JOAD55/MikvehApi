using System;
using MikvehApi.DTOs;

namespace MikvehApi.Services.Interfaces;

public interface ITrabajadorService : IGenericCrudService<TrabajadorDto, CreateTrabajadorDto, UpdateTrabajadorDto>
{
    Task<TrabajadorDto?> GetByUserAsync(string user);
}
