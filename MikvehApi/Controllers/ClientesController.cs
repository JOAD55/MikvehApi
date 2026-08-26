using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MikvehApi.DTOs;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientesController(IClienteService clienteService) : ControllerBase
{
    private readonly IClienteService _clienteService = clienteService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clientes = await _clienteService.GetAllAsync();

        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);

        return cliente is null ? NotFound() : Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClienteDto dto)
    {
        var cliente = await _clienteService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new {id = cliente.ClienteId}, cliente);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateClienteDto dto)
    {
        var cliente = await _clienteService.UpdateAsync(id, dto);

        return cliente is null ? NotFound() : Ok(cliente);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cliente = await _clienteService.DeleteAsync(id);

        return !cliente ? NotFound() : NoContent();
    }
}
