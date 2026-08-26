using System;
using MikvehApi.Models;

namespace MikvehApi.Repositories.Interfaces;

public interface ICitaRepository : IRepository<Cita>
{
    Task<Cita?> GetWithDetallesAsync(int id);
    Task<IEnumerable<Cita>> GetAllWithDetailsAsync();
    Task<IEnumerable<Cita>> GetByPeriodAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Cita>> GetByPeriodWithDetailsAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<Cita>> GetFuturasAsync();
}
