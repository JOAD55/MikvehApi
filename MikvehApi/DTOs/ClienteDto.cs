using System;

namespace MikvehApi.DTOs;

public class ClienteDto
{
    public int ClienteId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public DateTime? FechaNacimiento { get; set; }
}

public class CreateClienteDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public DateTime? FechaNacimiento { get; set; }
}

public class UpdateClienteDto
{
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public DateTime? FechaNacimiento { get; set; }
}
