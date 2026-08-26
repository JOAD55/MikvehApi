using System;
using AutoMapper;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class CitaService(
    ICitaRepository citaRepository,
    IDetalleCitaRepository detalleCitaRepository,
    IServicioRepository servicioRepository,
    IPaqueteRepository paqueteRepository,
    IMapper mapper) : ICitaService
{
    private readonly ICitaRepository _citaRepository = citaRepository;
    private readonly IDetalleCitaRepository _detalleCitaRepository = detalleCitaRepository;
    private readonly IServicioRepository _servicioRepository = servicioRepository;
    private readonly IPaqueteRepository _paqueteRepository = paqueteRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<CitaDto> CreateAsync(CreateCitaDto dto)
    {
        var cita = _mapper.Map<Cita>(dto);

        await _citaRepository.AddAsync(cita);

        return _mapper.Map<CitaDto>(cita);
    }

    public Task<bool> DeleteAsync(int id) => DeleteAsync(id, trabajadorActualId: null, esAdmin: true);

    public async Task<bool> DeleteAsync(int id, int? trabajadorActualId, bool esAdmin)
    {
        var cita = await _citaRepository.GetByIdAsync(id);

        if (cita is null) return false;

        VerificarPermisoModificacion(cita, trabajadorActualId, esAdmin);

        await _citaRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<CitaDto>> GetAllAsync()
    {
        var citas = await _citaRepository.GetAllAsync();

        return citas.Select(c => CitaDto.FromEntity(c));
    }

    public async Task<IEnumerable<DetailCitaDto>> GetAllWithDetailsAsync()
    {
        var citas = await _citaRepository.GetAllWithDetailsAsync();

        return citas.Select(c => _mapper.Map<DetailCitaDto>(c));
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

    public async Task<IEnumerable<CitaDto>> GetFuturasAsync()
    {
        var citas = await _citaRepository.GetFuturasAsync();

        return citas.Select(c => CitaDto.FromEntity(c));
    }

    public async Task<IEnumerable<CitaDto>> GetByWeekAsync(DateTime? referenceDate)
    {
        var fecha = (referenceDate ?? DateTime.Now).Date;

        int diasDesdeLunes = ((int)fecha.DayOfWeek + 6) % 7;
        var inicioSemana = fecha.AddDays(-diasDesdeLunes);
        var finSemana = inicioSemana.AddDays(7).AddTicks(-1);

        return await GetByPeriodAsync(inicioSemana, finSemana);
    }

    public async Task<IEnumerable<CitaDto>> GetByMonthAsync(DateTime? referenceDate)
    {
        var fecha = (referenceDate ?? DateTime.Now).Date;

        var inicioMes = new DateTime(fecha.Year, fecha.Month, 1);
        var finMes = inicioMes.AddMonths(1).AddTicks(-1);

        return await GetByPeriodAsync(inicioMes, finMes);
    }

    public async Task<DetailCitaDto?> GetWithDetailsAsync(int id)
    {
        var cita = await _citaRepository.GetWithDetallesAsync(id);

        if (cita is null) return null;

        return _mapper.Map<DetailCitaDto>(cita);
    }

    public Task<CitaDto?> UpdateAsync(int id, UpdateCitaDto dto) => UpdateAsync(id, dto, trabajadorActualId: null, esAdmin: true);

    public async Task<CitaDto?> UpdateAsync(int id, UpdateCitaDto dto, int? trabajadorActualId, bool esAdmin)
    {
        var cita = await _citaRepository.GetByIdAsync(id);

        if (cita is null) return null;

        VerificarPermisoModificacion(cita, trabajadorActualId, esAdmin);

        if (dto.FechaHoraCita is not null) cita.FechaHoraCita = dto.FechaHoraCita.Value;
        if (dto.Descripcion is not null) cita.Descripcion = dto.Descripcion;
        if (dto.TrabajadorId is not null) cita.TrabajadorId = dto.TrabajadorId;

        await _citaRepository.UpdateAsync(cita);

        return _mapper.Map<CitaDto>(cita);
    }

    public async Task<DetalleCitaDto?> CreateDetalleAsync(CreateDetalleCitaDto dto, int? trabajadorActualId, bool esAdmin)
    {
        var cita = await _citaRepository.GetByIdAsync(dto.CitaId);
        if (cita is null) return null;

        VerificarPermisoModificacion(cita, trabajadorActualId, esAdmin);

        bool tieneServicio = dto.ServicioId is not null;
        bool tienePaquete = dto.PaqueteId is not null;
        if (tieneServicio == tienePaquete) return null;

        decimal precioUnitario;

        if (tieneServicio)
        {
            var servicio = await _servicioRepository.GetByIdAsync(dto.ServicioId!.Value);
            if (servicio is null) return null;
            precioUnitario = servicio.PrecioBase;
        }
        else
        {
            var paquete = await _paqueteRepository.GetByIdAsync(dto.PaqueteId!.Value);
            if (paquete is null) return null;
            precioUnitario = paquete.Precio;
        }

        var detalle = _mapper.Map<DetalleCita>(dto);
        detalle.Subtotal = precioUnitario * dto.Cantidad;

        await _detalleCitaRepository.AddAsync(detalle);

        await RecalcularTotalPagarAsync(dto.CitaId);

        var detalleCreado = await _detalleCitaRepository.GetByIdAsync(detalle.DetalleCitaId);
        return _mapper.Map<DetalleCitaDto>(detalleCreado);
    }

    public async Task<bool> DeleteDetalleAsync(int id, int? trabajadorActualId, bool esAdmin)
    {
        var detalle = await _detalleCitaRepository.GetByIdAsync(id);
        if (detalle is null) return false;

        var cita = await _citaRepository.GetByIdAsync(detalle.CitaId);
        if (cita is not null) VerificarPermisoModificacion(cita, trabajadorActualId, esAdmin);

        int citaId = detalle.CitaId;

        await _detalleCitaRepository.DeleteAsync(id);

        await RecalcularTotalPagarAsync(citaId);

        return true;
    }

    public async Task<DetalleCitaDto?> GetDetalleByIdAsync(int id)
    {
        var detalle = await _detalleCitaRepository.GetByIdAsync(id);

        return detalle is null ? null : _mapper.Map<DetalleCitaDto>(detalle);
    }

    private static void VerificarPermisoModificacion(Cita cita, int? trabajadorActualId, bool esAdmin)
    {
        if (esAdmin) return;

        bool esPropia = cita.TrabajadorId is not null && cita.TrabajadorId == trabajadorActualId;
        bool esFutura = cita.FechaHoraCita >= DateTime.Now;

        if (!esPropia || !esFutura)
        {
            throw new UnauthorizedAccessException(
                "Solo puede modificar sus propias citas futuras.");
        }
    }

    private async Task RecalcularTotalPagarAsync(int citaId)
    {
        var cita = await _citaRepository.GetWithDetallesAsync(citaId);
        if (cita is null) return;

        cita.TotalPagar = cita.DetallesCita.Sum(d => d.Subtotal);

        await _citaRepository.UpdateAsync(cita);
    }
}
