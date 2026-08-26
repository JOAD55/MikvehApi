using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikvehApi.DTOs;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaquetesController(IPaqueteService paqueteService) : ControllerBase
{
    private readonly IPaqueteService _paqueteService = paqueteService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var paquetes = await _paqueteService.GetAllAsync();

        return Ok(paquetes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var paquete = await _paqueteService.GetByIdAsync(id);

        return paquete is null? NotFound() : Ok(paquete);
    }

    [HttpGet("detalles/{id}")]
    public async Task<IActionResult> GetDetalleById(int id)
    {
        var detalle = await _paqueteService.GetDetalleByIdAsync(id);

        return detalle is null? NotFound() : Ok(detalle);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePaqueteDto dto)
    {
        var paquete = await _paqueteService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = paquete.PaqueteId}, paquete);
    }

    [HttpPost("detalles")]
    public async Task<IActionResult> CreateDetalle(CreateDetallePaqueteDto dto)
    {
        var detalle = await _paqueteService.CreateDetalleAsync(dto);

        if (detalle is null) return BadRequest();

        return CreatedAtAction(nameof(GetDetalleById), new {id = detalle?.DetallePaqueteId}, detalle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePaqueteDto dto)
    {
        var paquete = await _paqueteService.UpdateAsync(id, dto);

        return paquete is null? NotFound() : Ok(paquete);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var paquete = await _paqueteService.DeleteAsync(id);

        return !paquete ? NotFound() : NoContent();
    }

    [HttpDelete("detalles/{id}")]
    public async Task<IActionResult> DeleteDetalle(int id)
    {
        var detalle = await _paqueteService.DeleteDetalleAsync(id);

        return !detalle ? NotFound() : NoContent();
    }
}
