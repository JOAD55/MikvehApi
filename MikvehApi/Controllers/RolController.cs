using Microsoft.AspNetCore.Mvc;
using MikvehApi.DTOs;
using MikvehApi.Models;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IRolService rolService) : ControllerBase
{
    private readonly IRolService _rolservice = rolService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _rolservice.GetAllAsync();

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var rol = await _rolservice.GetByIdAsync(id);

        if (rol is null) return NotFound();
        return Ok(rol);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CrearRolDto dto)
    {
        var rol = await _rolservice.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = rol.RolId}, rol);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ActualizarRolDto dto)
    {
        var rol = await _rolservice.UpdateAsync(id, dto);

        if (rol is null) return NotFound();
        return Ok(rol);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var rol = await _rolservice.DeleteAsync(id);

        if (!rol) return NotFound();
        return NoContent();
    }
}
