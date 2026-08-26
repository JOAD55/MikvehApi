using System.ComponentModel.DataAnnotations;
using MikvehApi.Models;

namespace MikvehApi.DTOs;

public class CitaDto
{
    public int CitaId { get; set; }
    public DateTime FechaHoraCita { get; set; }
    public string? Descripcion { get; set; }
    public decimal TotalPagar { get; set; }
    public int ClienteId { get; set; }
    public string? ClienteNombre { get; set; }
    public string? ClienteApellidos { get; set; }

    public int? TrabajadorId { get; set; }
    public string? TrabajadorNombre { get; set; }

    public static CitaDto FromEntity(Cita cita) => new CitaDto
    {
        CitaId = cita.CitaId,
        FechaHoraCita = cita.FechaHoraCita,
        Descripcion = cita.Descripcion?? string.Empty,
        TotalPagar = cita.TotalPagar,
        ClienteId = cita.ClienteId,
        ClienteNombre = cita.Cliente?.Nombre ?? string.Empty,
        ClienteApellidos = cita.Cliente?.Apellidos ?? string.Empty,
        TrabajadorId = cita.TrabajadorId,
        TrabajadorNombre = cita.Trabajador?.Nombre ?? string.Empty
    };
}

public class DetailCitaDto
{
    public int CitaId { get; set; }
    public DateTime FechaHoraCita { get; set; }
    public string? Descripcion { get; set; }
    public decimal TotalPagar { get; set; }
    public SummaryClienteDto Cliente { get; set; } = null!;
    public SummaryTrabajadorDto? Trabajador { get; set; }
    public ICollection<DetalleCitaDto> DetallesCita { get; set; } = [];
}

public class CreateCitaDto
{
    [Required(ErrorMessage = "El campo de fecha es obligatorio")]
    public DateTime FechaHoraCita { get; set; }
    public string? Descripcion { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "El cliente es obligatorio")]
    public int ClienteId { get; set; }
    public int? TrabajadorId { get; set; }
}

public class UpdateCitaDto
{
    public DateTime? FechaHoraCita { get; set; }
    public string? Descripcion { get; set; }
    public int? TrabajadorId { get; set; }
}
