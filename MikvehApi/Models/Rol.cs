using System;

namespace MikvehApi.Models;

public class Rol
{
    public int RolId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }

    public ICollection<Trabajador> Trabajadores { get; set; } = [];
}
