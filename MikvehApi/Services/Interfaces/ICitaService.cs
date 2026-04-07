using System;
using MikvehApi.DTOs;
using MikvehApi.Models;

namespace MikvehApi.Services.Interfaces;

public interface ICitaService : IGenericCrudService<CitaDto, CreateCitaDto, UpdateCitaDto>
{
    Task<IEnumerable<CitaDto>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    Task<DetailCitaDto?> GetWithDetailsAsync(int id);
    Task<IEnumerable<DetailCitaDto>> GetAllWithDetailsAsync();

}
