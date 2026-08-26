using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikvehApi.DTOs;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthController(IAuthService authService) : ControllerBase
{
    private readonly IAuthService _authService = authService;

    private int? TrabajadorIdActual =>
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var resultado = await _authService.LoginAsync(dto);

        return resultado is null ? Unauthorized() : Ok(resultado);
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var id = TrabajadorIdActual;
        if (id is null) return Unauthorized();

        var perfil = await _authService.GetPerfilAsync(id.Value);

        return perfil is null ? NotFound() : Ok(perfil);
    }

    [HttpPut("cambiar-password")]
    public async Task<IActionResult> CambiarPassword(CambiarPasswordDto dto)
    {
        var id = TrabajadorIdActual;
        if (id is null) return Unauthorized();

        var exito = await _authService.CambiarPasswordAsync(id.Value, dto);

        return exito ? NoContent() : BadRequest("La contrasena actual no es correcta.");
    }
}
