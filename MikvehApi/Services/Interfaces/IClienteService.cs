using System;
using MikvehApi.DTOs;

namespace MikvehApi.Services.Interfaces;

public interface IClienteService : IGenericCrudService<ClienteDto, CreateClienteDto, UpdateClienteDto>
{
    
}
