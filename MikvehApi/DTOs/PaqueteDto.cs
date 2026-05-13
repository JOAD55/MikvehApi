using System;

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
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; } = 0;
}

public class UpdatePaqueteDto
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal? Precio { get; set; }
}

public class SummaryPaqueteDto
{
    public int PaqueteId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}