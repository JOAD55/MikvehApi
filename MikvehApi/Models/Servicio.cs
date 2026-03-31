using System;

namespace MikvehApi.Models;

public class Servicio
{
    public int ServicioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int DuracionMinutos { get; set; }
    public decimal PrecioBase { get; set; } = 0;

    public ICollection<DetallePaquete> DetallesPaquete { get; set; } = [];
    public ICollection<DetalleCita> DetallesCita { get; set; } = [];
}
