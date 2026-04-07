using System;
using Microsoft.AspNetCore.Mvc;
using MikvehApi.DTOs;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitasController(ICitaService citaService) : ControllerBase
{
    private readonly ICitaService _citaService = citaService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var citas = await _citaService.GetAllAsync();

        return Ok(citas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cita = await _citaService.GetWithDetailsAsync(id);

        return cita is null ? NotFound() : Ok(cita);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCitaDto dto)
    {
        var cita = await _citaService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = cita.CitaId}, cita);
    }

    [HttpPatch]
    public async Task<IActionResult> Update(int id, UpdateCitaDto dto)
    {
        var cita = await _citaService.UpdateAsync(id, dto);

        return cita is null ? NotFound() : Ok(cita);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cita = await _citaService.DeleteAsync(id);

        return !cita ? NotFound() : NoContent();
    }
}
