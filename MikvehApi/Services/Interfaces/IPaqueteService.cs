using System;
using MikvehApi.DTOs;

namespace MikvehApi.Services.Interfaces;

public interface IPaqueteService : IGenericCrudService<PaqueteDto, CreatePaqueteDto, UpdatePaqueteDto>
{
    Task<DetalleCitaDto> CreateDetalleAsync(CreateDetallePaqueteDto dto);
    Task<DetalleCitaDto> DeleteDetalleAsync(int id);
}
