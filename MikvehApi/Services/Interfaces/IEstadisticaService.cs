using System;
using MikvehApi.DTOs;

namespace MikvehApi.Services.Interfaces;

public interface IEstadisticaService
{
    Task<ResumenEstadisticasDto> GetResumenAsync(DateTime? inicio, DateTime? fin);
}
