using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikvehApi.Constants;
using MikvehApi.DTOs;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CitasController(ICitaService citaService) : ControllerBase
{
    private readonly ICitaService _citaService = citaService;

    private bool EsAdmin => User.IsInRole(Roles.Administrador);

    private int? TrabajadorIdActual =>
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var citas = await _citaService.GetAllAsync();

        return Ok(citas);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cita = await _citaService.GetWithDetailsAsync(id);

        return cita is null ? NotFound() : Ok(cita);
    }

    [HttpGet("con-detalles")]
    public async Task<IActionResult> GetAllWithDetails()
    {
        var citas = await _citaService.GetAllWithDetailsAsync();

        return Ok(citas);
    }

    [HttpGet("futuras")]
    public async Task<IActionResult> GetFuturas()
    {
        var citas = await _citaService.GetFuturasAsync();

        return Ok(citas);
    }

    [HttpGet("semana")]
    public async Task<IActionResult> GetSemana([FromQuery] DateTime? fecha)
    {
        var citas = await _citaService.GetByWeekAsync(fecha);

        return Ok(citas);
    }

    [HttpGet("mes")]
    public async Task<IActionResult> GetMes([FromQuery] DateTime? fecha)
    {
        var citas = await _citaService.GetByMonthAsync(fecha);

        return Ok(citas);
    }

    [HttpGet("periodo")]
    public async Task<IActionResult> GetPeriodo([FromQuery] DateTime inicio, [FromQuery] DateTime fin)
    {
        if (fin < inicio) return BadRequest("La fecha final no puede ser anterior a la fecha de inicio.");

        var citas = await _citaService.GetByPeriodAsync(inicio, fin);

        return Ok(citas);
    }

    [HttpGet("detalles/{id}")]
    public async Task<IActionResult> GetDetalleById(int id)
    {
        var detalle = await _citaService.GetDetalleByIdAsync(id);

        return detalle is null ? NotFound() : Ok(detalle);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCitaDto dto)
    {
        var cita = await _citaService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = cita.CitaId}, cita);
    }

    [HttpPost("detalles")]
    public async Task<IActionResult> CreateDetalle(CreateDetalleCitaDto dto)
    {
        var detalle = await _citaService.CreateDetalleAsync(dto, TrabajadorIdActual, EsAdmin);

        if (detalle is null) return BadRequest();

        return CreatedAtAction(nameof(GetDetalleById), new {id = detalle.DetalleCitaId}, detalle);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCitaDto dto)
    {
        var cita = await _citaService.UpdateAsync(id, dto, TrabajadorIdActual, EsAdmin);

        return cita is null ? NotFound() : Ok(cita);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cita = await _citaService.DeleteAsync(id, TrabajadorIdActual, EsAdmin);

        return !cita ? NotFound() : NoContent();
    }

    [HttpDelete("detalles/{id}")]
    public async Task<IActionResult> DeleteDetalle(int id)
    {
        var detalle = await _citaService.DeleteDetalleAsync(id, TrabajadorIdActual, EsAdmin);

        return !detalle ? NotFound() : NoContent();
    }
}
