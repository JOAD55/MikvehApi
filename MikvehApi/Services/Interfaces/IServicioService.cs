using System;
using MikvehApi.DTOs;

namespace MikvehApi.Services.Interfaces;

public interface IServicioService : IGenericCrudService<ServicioDto, CreateServicioDto, UpdateServicioDto>
{
    
}
