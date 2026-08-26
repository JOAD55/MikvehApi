using System;
using System.ComponentModel.DataAnnotations;

namespace MikvehApi.DTOs;

public class DetalleCitaDto
{
    public int DetalleCitaId { get; set; }
    public int CitaId { get; set; }
    public int Cantidad { get; set; }
    public decimal Subtotal { get; set; } = 0;
    public SummaryServicioDto? Servicio { get; set; }
    public SummaryPaqueteDto? Paquete { get; set; }
}

public class CreateDetalleCitaDto
{
    [Required(ErrorMessage = "El campo cita es obligatorio")]
    public int CitaId { get; set; }

    public int? ServicioId { get; set; }
    public int? PaqueteId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
    public int Cantidad { get; set; } = 1;
}
