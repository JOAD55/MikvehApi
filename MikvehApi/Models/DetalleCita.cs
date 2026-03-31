using System;

namespace MikvehApi.Models;

public class DetalleCita
{
    public int DetalleCitaId { get; set; }
    public int CitaId { get; set; }
    public Cita Cita { get; set; } = null!;

    public int? ServicioId { get; set; }
    public Servicio? Servicio { get; set; }

    public int? PaqueteId { get; set; }
    public Paquete? Paquete { get; set; }

    public int Cantidad { get; set; } = 1;

    public decimal Subtotal { get; set; } = 0;
}
