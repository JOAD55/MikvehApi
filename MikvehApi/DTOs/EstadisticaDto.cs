using System;

namespace MikvehApi.DTOs;

public class ResumenEstadisticasDto
{
    public DateTime Inicio { get; set; }
    public DateTime Fin { get; set; }
    public int TotalCitas { get; set; }
    public decimal TotalIngresos { get; set; }
    public decimal PromedioPorCita { get; set; }
    public ICollection<IngresosPorTrabajadorDto> IngresosPorTrabajador { get; set; } = [];
    public ICollection<ServicioMasSolicitadoDto> ServiciosMasSolicitados { get; set; } = [];
}

public class IngresosPorTrabajadorDto
{
    public int? TrabajadorId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public int TotalCitas { get; set; }
    public decimal TotalIngresos { get; set; }
}

public class ServicioMasSolicitadoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int CantidadSolicitada { get; set; }
    public decimal TotalIngresos { get; set; }
}
