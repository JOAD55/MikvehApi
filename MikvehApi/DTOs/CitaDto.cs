using MikvehApi.Models;

namespace MikvehApi.DTOs;

public class CitaDto
{
    public int CitaId { get; set; }
    public DateTime FechaHoraCita { get; set; }
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
        TotalPagar = cita.TotalPagar,
        ClienteId = cita.ClienteId,
        ClienteNombre = cita.Cliente?.Nombre ?? string.Empty,
        ClienteApellidos = cita.Cliente?.Apellidos ?? string.Empty,
        TrabajadorId = cita.TrabajadorId,
        TrabajadorNombre = cita.Trabajador?.Nombre ?? string.Empty
    };
}
