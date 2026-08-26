using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TrabajadoresController(ITrabajadorService trabajadorService) : ControllerBase
{
    private readonly ITrabajadorService _trabajadorService = trabajadorService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trabajadores = await _trabajadorService.GetAllAsync();

        return Ok(trabajadores);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var trabajador = await _trabajadorService.GetByIdAsync(id);

        if (trabajador is null) return NotFound();
        return Ok(trabajador);
    }

    [HttpGet("by-user/{user}")]
    public async Task<IActionResult> GetByUser(string user)
    {
        var trabajador = await _trabajadorService.GetByUserAsync(user);

        if (trabajador is null) return NotFound();
        return Ok(trabajador);
    }

    [HttpPost]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Create(CreateTrabajadorDto dto)
    {
        var trabajador = await _trabajadorService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = trabajador.TrabajadorId}, trabajador);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Update(int id, UpdateTrabajadorDto dto)
    {
        var trabajador = await _trabajadorService.UpdateAsync(id , dto);

        return trabajador is null ? NotFound() : Ok(trabajador);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        var trabajador = await _trabajadorService.DeleteAsync(id);

        return !trabajador ? NotFound() : NoContent();
    }
}
