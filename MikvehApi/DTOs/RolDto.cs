using System;

namespace MikvehApi.DTOs;

public class RolDto
{
    public int RolId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class CrearRolDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class ActualizarRolDto
{
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
}
