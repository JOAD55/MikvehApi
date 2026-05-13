using System;
using MikvehApi.DTOs;

namespace MikvehApi.Services.Interfaces;

public interface IPaqueteService : IGenericCrudService<PaqueteDto, CreatePaqueteDto, UpdatePaqueteDto>
{

}
