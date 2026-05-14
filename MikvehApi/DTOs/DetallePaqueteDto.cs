using System;

namespace MikvehApi.DTOs;

public class DetallePaqueteDto
{
    public int DetallePaqueteId { get; set; }
    public string? NombrePaquete { get; set; }
    public string? NombreServicio { get; set; }
}

public class CreateDetallePaqueteDto
{
    public int PaqueteId { get; set; }
    public int ServicioId { get; set; }
}


