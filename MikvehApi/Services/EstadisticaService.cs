using System;
using MikvehApi.DTOs;
using MikvehApi.Repositories.Interfaces;
using MikvehApi.Services.Interfaces;

namespace MikvehApi.Services;

public class EstadisticaService(ICitaRepository citaRepository) : IEstadisticaService
{
    private readonly ICitaRepository _citaRepository = citaRepository;

    public async Task<ResumenEstadisticasDto> GetResumenAsync(DateTime? inicio, DateTime? fin)
    {
        var hoy = DateTime.Now.Date;
        var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
        var finMes = inicioMes.AddMonths(1).AddTicks(-1);

        var desde = inicio ?? inicioMes;
        var hasta = fin ?? finMes;

        var citas = (await _citaRepository.GetByPeriodWithDetailsAsync(desde, hasta)).ToList();

        var totalCitas = citas.Count;
        var totalIngresos = citas.Sum(c => c.TotalPagar);

        var ingresosPorTrabajador = citas
            .GroupBy(c => c.TrabajadorId)
            .Select(g => new IngresosPorTrabajadorDto
            {
                TrabajadorId = g.Key,
                NombreCompleto = g.First().Trabajador is not null
                    ? $"{g.First().Trabajador!.Nombre} {g.First().Trabajador!.Apellidos}"
                    : "Sin asignar",
                TotalCitas = g.Count(),
                TotalIngresos = g.Sum(c => c.TotalPagar)
            })
            .OrderByDescending(i => i.TotalIngresos)
            .ToList();

        var serviciosMasSolicitados = citas
            .SelectMany(c => c.DetallesCita)
            .GroupBy(d => d.ServicioId is not null
                ? ("Servicio", d.Servicio!.Nombre)
                : ("Paquete", d.Paquete!.Nombre))
            .Select(g => new ServicioMasSolicitadoDto
            {
                Tipo = g.Key.Item1,
                Nombre = g.Key.Item2,
                CantidadSolicitada = g.Sum(d => d.Cantidad),
                TotalIngresos = g.Sum(d => d.Subtotal)
            })
            .OrderByDescending(s => s.CantidadSolicitada)
            .Take(10)
            .ToList();

        return new ResumenEstadisticasDto
        {
            Inicio = desde,
            Fin = hasta,
            TotalCitas = totalCitas,
            TotalIngresos = totalIngresos,
            PromedioPorCita = totalCitas > 0 ? totalIngresos / totalCitas : 0,
            IngresosPorTrabajador = ingresosPorTrabajador,
            ServiciosMasSolicitados = serviciosMasSolicitados
        };
    }
}
