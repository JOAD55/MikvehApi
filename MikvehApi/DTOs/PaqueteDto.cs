using System;

namespace MikvehApi.DTOs;

public class PaqueteDto
{

}

public class SummaryPaqueteDto
{
    public int PaqueteId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}