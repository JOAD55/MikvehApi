using System;
using System.ComponentModel.DataAnnotations;

namespace MikvehApi.DTOs;

public class DetallePaqueteDto
{
    public int DetallePaqueteId { get; set; }
    public string? NombrePaquete { get; set; }
    public string? NombreServicio { get; set; }
}

public class CreateDetallePaqueteDto
{
    [Range(1, int.MaxValue, ErrorMessage = "El paquete es obligatorio")]
    public int PaqueteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El servicio es obligatorio")]
    public int ServicioId { get; set; }
}


