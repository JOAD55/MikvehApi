using System;

namespace MikvehApi.DTOs;

public class ServicioDto
{

}

public class SummaryServicioDto
{
    public int ServicioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal PrecioBase { get; set; }
}
