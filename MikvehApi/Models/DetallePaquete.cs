using System;

namespace MikvehApi.Models;

public class DetallePaquete
{
    public int DetallePaqueteId { get; set; }

    public int? PaqueteId { get; set; }
    public Paquete? Paquete { get; set; } = null!;

    public int? ServicioId { get; set; }
    public Servicio? Servicio { get; set; } = null!;
}
