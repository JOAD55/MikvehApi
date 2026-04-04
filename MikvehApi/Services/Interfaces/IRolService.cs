using System;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Services.Interfaces;

public interface IRolService : IGenericCrudService<RolDto, CrearRolDto, ActualizarRolDto> { }
