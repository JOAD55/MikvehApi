using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikvehApi.Constants;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = Roles.Administrador)]
public class EstadisticasController(IEstadisticaService estadisticaService) : ControllerBase
{
    private readonly IEstadisticaService _estadisticaService = estadisticaService;

    [HttpGet("resumen")]
    public async Task<IActionResult> GetResumen([FromQuery] DateTime? inicio, [FromQuery] DateTime? fin)
    {
        if (inicio is not null && fin is not null && fin < inicio)
        {
            return BadRequest("La fecha final no puede ser anterior a la fecha de inicio.");
        }

        var resumen = await _estadisticaService.GetResumenAsync(inicio, fin);

        return Ok(resumen);
    }
}
