using System;
using System.ComponentModel.DataAnnotations;

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

public class SummaryClienteDto
{
    public int ClienteId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
}

public class CreateClienteDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(255, ErrorMessage = "El nombre no debe superar los 255 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos son obligatorios")]
    [MaxLength(255, ErrorMessage = "Los apellidos no deben superar los 255 caracteres")]
    public string Apellidos { get; set; } = string.Empty;

    [Phone]
    public string? Telefono { get; set; }

    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }
}

public class UpdateClienteDto
{
    [MaxLength(255, ErrorMessage = "El nombre no debe superar los 255 caracteres")]
    public string? Nombre { get; set; }

    [MaxLength(255, ErrorMessage = "Los apellidos no deben superar los 255 caracteres")]
    public string? Apellidos { get; set; }

    [Phone]
    public string? Telefono { get; set; }

    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }
}
