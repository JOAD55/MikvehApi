using System;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Services.Interfaces;

public interface ICitaService : IGenericCrudService<CitaDto, CreateCitaDto, UpdateCitaDto>
{
    Task<IEnumerable<CitaDto>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<CitaDto>> GetFuturasAsync();
    Task<IEnumerable<CitaDto>> GetByWeekAsync(DateTime? referenceDate);
    Task<IEnumerable<CitaDto>> GetByMonthAsync(DateTime? referenceDate);
    Task<DetailCitaDto?> GetWithDetailsAsync(int id);
    Task<IEnumerable<DetailCitaDto>> GetAllWithDetailsAsync();

    Task<DetalleCitaDto?> CreateDetalleAsync(CreateDetalleCitaDto dto);
    Task<bool> DeleteDetalleAsync(int id);
    Task<DetalleCitaDto?> GetDetalleByIdAsync(int id);
}
