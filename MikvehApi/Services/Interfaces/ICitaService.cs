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

    /// <summary>
    /// Actualiza una cita validando permisos: un Administrador puede modificar cualquier
    /// cita; cualquier otro rol solo puede modificar sus propias citas futuras.
    /// Lanza UnauthorizedAccessException si no tiene permiso.
    /// </summary>
    Task<CitaDto?> UpdateAsync(int id, UpdateCitaDto dto, int? trabajadorActualId, bool esAdmin);

    /// <summary>
    /// Elimina una cita validando permisos con la misma regla que UpdateAsync.
    /// </summary>
    Task<bool> DeleteAsync(int id, int? trabajadorActualId, bool esAdmin);

    Task<DetalleCitaDto?> CreateDetalleAsync(CreateDetalleCitaDto dto, int? trabajadorActualId, bool esAdmin);
    Task<bool> DeleteDetalleAsync(int id, int? trabajadorActualId, bool esAdmin);
    Task<DetalleCitaDto?> GetDetalleByIdAsync(int id);
}
