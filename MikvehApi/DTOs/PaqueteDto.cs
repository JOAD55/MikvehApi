using System;
using System.ComponentModel.DataAnnotations;

namespace MikvehApi.DTOs;

public class PaqueteDto
{
    public int PaqueteId { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; }
}

public class CreatePaqueteDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
    public decimal Precio { get; set; } = 0;
}

public class UpdatePaqueteDto
{
    [MaxLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres")]
    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "El precio no puede ser negativo")]
    public decimal? Precio { get; set; }
}

public class SummaryPaqueteDto
{
    public int PaqueteId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}