using System;
using System.ComponentModel.DataAnnotations;
using MikvehApi.Models;

namespace MikvehApi.DTOs;

public class TrabajadorDto
{
    public int TrabajadorId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public DateTime? FechaNacimiento { get; set; }

    public int? RolId { get; set; }
    public string? RolNombre { get; set; }
}

public class SummaryTrabajadorDto
{
    public int TrabajadorId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
}

public class CreateTrabajadorDto
{
    [Required]
    [MaxLength(255)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string Apellidos { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Usuario { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = string.Empty;

    [Phone]
    public string? Telefono { get; set; }

    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? FechaNacimiento { get; set; }

    [Range(1, int.MaxValue)]
    public int RolId { get; set; }
}

public class UpdateTrabajadorDto
{
    [MaxLength(255)]
    public string? Nombre { get; set; }

    [MaxLength(255)]
    public string? Apellidos { get; set; }

    [Phone]
    public string? Telefono { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? FechaNacimiento { get; set; }
    
    [Range(1, int.MaxValue)]
    public int? RolId { get; set; }
}
