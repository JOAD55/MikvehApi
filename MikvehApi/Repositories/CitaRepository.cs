using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class CitaRepository : Repository<Cita>, ICitaRepository
{
    public CitaRepository(AppDbContext context) : base(context) { }

    public override async Task<Cita?> GetByIdAsync(int id)
    {
        return await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Trabajador)
                .FirstOrDefaultAsync(c => c.CitaId == id);
    }

    public override async Task<IEnumerable<Cita>> GetAllAsync()
    {
        return await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Trabajador)
                .ToListAsync();
    }

    public async Task<IEnumerable<Cita>> GetAllWithDetailsAsync()
    {
        return await _context.Citas
                .Include(c => c.Cliente)
                .Include(c => c.Trabajador)
                .Include(c => c.DetallesCita)
                    .ThenInclude(d => d.Servicio)
                .Include(c => c.DetallesCita)
                    .ThenInclude(d => d.Paquete)
                .ToListAsync();
    }

    public async Task<IEnumerable<Cita>> GetByPeriodAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.Citas.Include(c => c.Cliente)
            .Include(c => c.Trabajador)
            .Where(c => c.FechaHoraCita >= startDate && c.FechaHoraCita <= endDate)
            .OrderBy(c => c.FechaHoraCita)
            .ToListAsync();
    }

    public async Task<IEnumerable<Cita>> GetFuturasAsync()
    {
        return await _context.Citas.Include(c => c.Cliente)
            .Include(c => c.Trabajador)
            .Where(c => c.FechaHoraCita >= DateTime.Now)
            .OrderBy(c => c.FechaHoraCita)
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
