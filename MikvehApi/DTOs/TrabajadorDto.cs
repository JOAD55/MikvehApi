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

public class CreateTrabajadorDto
{
    [Required(ErrorMessage = "El campo nombre es obligatorio")]
    [MaxLength(255, ErrorMessage = "El nombre no debe superar los 255 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public DateTime? FechaNacimiento { get; set; }

    public int RolId { get; set; }
}

public class UpdateTrabajadorDto
{
    public string? Nombre { get; set; }
    public string? Apellidos { get; set; }
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public int? RolId { get; set; }
}
