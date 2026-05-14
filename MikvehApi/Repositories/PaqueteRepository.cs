using System;
using System.Runtime.Intrinsics.Arm;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class PaqueteRepository : Repository<Paquete>, IPaqueteRepository
{
    public PaqueteRepository(AppDbContext context) : base(context) { }

    public override async Task<Paquete?> GetByIdAsync(int id)
    {
        return await _context.Paquetes
            .Include(P => P.DetallesPaquete)
                .ThenInclude(Dp => Dp.Servicio)
            .FirstOrDefaultAsync(P => P.PaqueteId == id);
    }

    public override async Task<IEnumerable<Paquete>> GetAllAsync()
    {
        return await _context.Paquetes
            .Include(P => P.DetallesPaquete)
                .ThenInclude(Dp => Dp.Servicio)
            .ToListAsync();
    }

    public async Task AddDetalleAsync(DetallePaquete detallePaquete)
    {
        await _context.DetallesPaquete.AddAsync(detallePaquete);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDetalleAsync(int id)
    {
        var detalle = await _context.DetallesPaquete.FindAsync(id);
        if (detalle is not null)
        {
            _context.DetallesPaquete.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<DetallePaquete?> GetDetallaByIdAsync(int id)
    {
        return await _context.DetallesPaquete
            .Include(dp => dp.Paquete)
            .Include(dp => dp.Servicio)
            .FirstOrDefaultAsync(dp => dp.DetallePaqueteId == id);
    }
}
