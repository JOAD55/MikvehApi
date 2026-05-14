using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class PaqueteService(IPaqueteRepository paqueteRepository, IServicioRepository servicioRepository, IMapper mapper) : IPaqueteService
{
    private readonly IPaqueteRepository _paqueteRepository = paqueteRepository;
    private readonly IServicioRepository _servicioRepository = servicioRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<PaqueteDto> CreateAsync(CreatePaqueteDto dto)
    {
        var paquete = _mapper.Map<Paquete>(dto);

        await _paqueteRepository.AddAsync(paquete);

        return _mapper.Map<PaqueteDto>(paquete);
    }

    public async Task<DetallePaqueteDto?> CreateDetalleAsync(CreateDetallePaqueteDto dto)
    {
        var paquete = await GetByIdAsync(dto.PaqueteId);
        if (paquete is null) return null;

        var servicio = await _servicioRepository.GetByIdAsync(dto.ServicioId);
        if ( servicio is null) return null;

        var detallePaquete = _mapper.Map<DetallePaquete>(dto);

        await _paqueteRepository.AddDetalleAsync(detallePaquete);

        return _mapper.Map<DetallePaqueteDto>(detallePaquete);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var paquete = await _paqueteRepository.GetByIdAsync(id);

        if (paquete is null) return false;

        await _paqueteRepository.DeleteAsync(id);

        return true;
    }

    public async Task<bool> DeleteDetalleAsync(int id)
    {
        var detalle = await _paqueteRepository.GetDetallaByIdAsync(id);

        if (detalle is null) return false;

        await _paqueteRepository.DeleteDetalleAsync(id);
        return true;
    }

    public async Task<IEnumerable<PaqueteDto>> GetAllAsync()
    {
        var paquetes = await _paqueteRepository.GetAllAsync();

        return paquetes.Select(P => _mapper.Map<PaqueteDto>(P))
            .OrderBy(Pd => Pd.Nombre);
    }

    public async Task<PaqueteDto?> GetByIdAsync(int id)
    {
        var paquete = await _paqueteRepository.GetByIdAsync(id);

        if (paquete is null) return null;

        return _mapper.Map<PaqueteDto>(paquete);
    }

    public async Task<DetallePaqueteDto?> GetDetalleByIdAsync(int id)
    {
        var detalle = await _paqueteRepository.GetDetallaByIdAsync(id);

        if (detalle is null) return null;

        return _mapper.Map<DetallePaqueteDto>(detalle);
    }

    public async Task<PaqueteDto?> UpdateAsync(int id, UpdatePaqueteDto dto)
    {
        var paquete = await _paqueteRepository.GetByIdAsync(id);

        if (paquete is null) return null;

        if (dto.Nombre is not null) paquete.Nombre = dto.Nombre;
        if (dto.Descripcion is not null) paquete.Descripcion = dto.Descripcion;
        if (dto.Precio is not null) paquete.Precio = (decimal)dto.Precio;

        await _paqueteRepository.UpdateAsync(paquete);

        return _mapper.Map<PaqueteDto>(paquete);
    }
}
