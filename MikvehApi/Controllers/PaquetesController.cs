using System;
using Microsoft.AspNetCore.Mvc;
using MikvehApi.DTOs;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpPost]
    public async Task<IActionResult> Create(CreatePaqueteDto dto)
    {
        var paquete = await _paqueteService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = paquete.PaqueteId}, paquete);
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
}
