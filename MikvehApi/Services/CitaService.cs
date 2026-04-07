using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class CitaService(ICitaRepository citaRepository, IMapper mapper) : ICitaService
{
    private readonly ICitaRepository _citaRepository = citaRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CitaDto> CreateAsync(CreateCitaDto dto)
    {
        var cita = new Cita
        {
            FechaHoraCita = dto.FechaHoraCita,
            Descripcion = dto.Descripcion,
            ClienteId = dto.ClienteId,
            TrabajadorId = dto.TrabajadorId
        };

        await _citaRepository.AddAsync(cita);

        return CitaDto.FromEntity(cita);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cita = await _citaRepository.GetByIdAsync(id);

        if (cita is null) return false;

        await _citaRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<CitaDto>> GetAllAsync()
    {
        var citas = await _citaRepository.GetAllAsync();

        return citas.Select(c => CitaDto.FromEntity(c));
    }

    public Task<IEnumerable<DetailCitaDto>> GetAllWithDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CitaDto?> GetByIdAsync(int id)
    {
        var cita = await _citaRepository.GetByIdAsync(id);

        return cita is null ? null : CitaDto.FromEntity(cita);
    }

    public async Task<IEnumerable<CitaDto>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
    {
        var citas = await _citaRepository.GetByPeriodAsync(startDate, endDate);

        return citas.Select(c => CitaDto.FromEntity(c));
    }

    public async Task<DetailCitaDto?> GetWithDetailsAsync(int id)
    {
        var cita = await _citaRepository.GetWithDetallesAsync(id);

        if (cita is null) return null;

        return _mapper.Map<DetailCitaDto>(cita);
    }

    public async Task<CitaDto?> UpdateAsync(int id, UpdateCitaDto dto)
    {
        var cita = await _citaRepository.GetByIdAsync(id);

        if (cita is null) return null;

        if (dto.FechaHoraCita is not null) cita.FechaHoraCita = dto.FechaHoraCita.Value;
        if (dto.Descripcion is not null) cita.Descripcion = dto.Descripcion;
        if (dto.TrabajadorId is not null) cita.TrabajadorId = dto.TrabajadorId;

        await _citaRepository.UpdateAsync(cita);

        return CitaDto.FromEntity(cita);
    }
}
