using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class ServicioService(IServicioRepository servicioRepository, IMapper mapper) : IServicioService
{
    private readonly IServicioRepository _servicioRepository = servicioRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServicioDto> CreateAsync(CreateServicioDto dto)
    {
        var servicio = _mapper.Map<Servicio>(dto);

        await _servicioRepository.AddAsync(servicio);

        return _mapper.Map<ServicioDto>(servicio);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var servicio = await _servicioRepository.GetByIdAsync(id);

        if (servicio is null) return false;

        await _servicioRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<ServicioDto>> GetAllAsync()
    {
        var servicios = await _servicioRepository.GetAllAsync();

        return servicios.Select(s => _mapper.Map<ServicioDto>(s));
    }

    public async Task<ServicioDto?> GetByIdAsync(int id)
    {
        var servicio = await _servicioRepository.GetByIdAsync(id);

        if (servicio is null) return null;

        return _mapper.Map<ServicioDto>(servicio);
    }

    public async Task<ServicioDto?> UpdateAsync(int id, UpdateServicioDto dto)
    {
        var servicio = await _servicioRepository.GetByIdAsync(id);

        if (servicio is null) return null;

        _mapper.Map(dto, servicio);

        await _servicioRepository.UpdateAsync(servicio);

        return _mapper.Map<ServicioDto>(servicio);
    }
}
