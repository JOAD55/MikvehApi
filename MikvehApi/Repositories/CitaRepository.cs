using System;
using Microsoft.EntityFrameworkCore;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class CitaRepository : Repository<Cita>, ICitaRepository
{
    public CitaRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Cita>> GetAllAsync()
    {
        return await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Trabajador)
                .ToListAsync();
    }

    public async Task<IEnumerable<Cita>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Citas.Where(c => c.FechaHoraCita >= startDate && c.FechaHoraCita <= endDate).ToListAsync();
    }

    public async Task<IEnumerable<Cita>> GetByPeriodAndDetailsAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Citas.Include(c => c.Cliente)
                .Include(c => c.Trabajador)
                .Where(c => c.FechaHoraCita >= startDate && c.FechaHoraCita <= endDate)
                .ToListAsync();
    }

    public async Task<Cita?> GetWithDetallesAsync(int id)
    {
        return await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Trabajador)
                .Include(c => c.DetallesCita)
                    .ThenInclude(d => d.Servicio)
                .Include(c => c.DetallesCita)
                    .ThenInclude(d => d.Paquete)
                .FirstOrDefaultAsync(c => c.CitaId == id);
    }
}
