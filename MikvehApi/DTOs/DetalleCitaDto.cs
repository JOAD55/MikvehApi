using System;

namespace MikvehApi.DTOs;

public class DetalleCitaDto
{
    public int DetalleCitaId { get; set; }
    public int Cantidad { get; set; }
    public decimal Subtotal { get; set; } = 0;
    public SummaryServicioDto? Servicio { get; set; }
    public SummaryPaqueteDto? Paquete { get; set; }
}

public class CreateDetalleCitaDto
{
    
}
