using System;
using Microsoft.AspNetCore.Mvc;
using MikvehApi.DTOs;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiciosController(IServicioService servicioService) : ControllerBase
{
    private readonly IServicioService _servicioService = servicioService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var servicios = await _servicioService.GetAllAsync();

        return Ok(servicios);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var servicio = await _servicioService.GetByIdAsync(id);

        return servicio is null? NotFound() : Ok(servicio);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateServicioDto dto)
    {
        var servicio = await _servicioService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = servicio.ServicioId}, servicio);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateServicioDto dto)
    {
        var servicio = await _servicioService.UpdateAsync(id, dto);

        return servicio is null ? NotFound() : Ok(servicio);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var servicio = await _servicioService.DeleteAsync(id);

        return !servicio ? NotFound() : NoContent();
    }
}
