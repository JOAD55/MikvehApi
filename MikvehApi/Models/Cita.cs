using System;

namespace MikvehApi.Models;

public class Cita
{
    public int CitaId { get; set; }
    public DateTime FechaHoraCita { get; set; }
    public decimal TotalPagar { get; set; }
    public int? ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    public int? TrabajadorId { get; set; }
    public Trabajador? Trabajador { get; set; }

    public ICollection<DetalleCita> DetallesCita { get; set; } = [];
}
