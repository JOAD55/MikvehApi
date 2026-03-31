using System;

namespace MikvehApi.Models;

public class Trabajador
{
    public int TrabajadorId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string? Email { get; set; }
    public DateTime? FechaNacimiento { get; set; }

    public int RolId { get; set; }
    public Rol Rol { get; set; } = null!;

    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}
