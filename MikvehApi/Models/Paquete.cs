using System;

namespace MikvehApi.Models;

public class Paquete
{
    public int PaqueteId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public decimal Precio { get; set; } = 0;

    public ICollection<DetallePaquete> DetallesPaquete { get; set; } = [];
    public ICollection<DetalleCita> DetallesCita { get; set; } = [];
}
