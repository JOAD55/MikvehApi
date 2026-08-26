using System;
using System.ComponentModel.DataAnnotations;

namespace MikvehApi.DTOs;

public class ServicioDto
{
    public int ServicioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int DuracionMinutos { get; set; }
    public decimal PrecioBase { get; set; }
}

public class CreateServicioDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La duracion debe ser mayor a 0")]
    public int DuracionMinutos { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
    public decimal PrecioBase { get; set; } = 0;
}

public class UpdateServicioDto
{
    [MaxLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La duracion debe ser mayor a 0")]
    public int? DuracionMinutos { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
    public decimal? PrecioBase { get; set; }
}

public class SummaryServicioDto
{
    public int ServicioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioBase { get; set; }
}
