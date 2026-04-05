using System;
using System.ComponentModel.DataAnnotations;

namespace MikvehApi.DTOs;

public class RolDto
{
    public int RolId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class CreateRolDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "La descripcion no puede superar los 500 caracteres")]
    public string Descripcion { get; set; } = string.Empty;
}

public class UpdateRolDto
{
    [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
    public string? Nombre { get; set; }

    [MaxLength(500, ErrorMessage = "La descripcion no puede superar los 500 caracteres")]
    public string? Descripcion { get; set; }
}
