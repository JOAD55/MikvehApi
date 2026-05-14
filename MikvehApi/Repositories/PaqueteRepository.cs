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
                .ThenInclude(Dp => Dp.Paquete)
            .Include(P => P.DetallesPaquete)
                .ThenInclude(Dp => Dp.Servicio)
            .FirstOrDefaultAsync(P => P.PaqueteId == id);
    }

    public override async Task<IEnumerable<Paquete>> GetAllAsync()
    {
        return await _context.Paquetes
            .Include(P => P.DetallesPaquete)
                .ThenInclude(Dp => Dp.Paquete)
            .Include(P => P.DetallesPaquete)
                .ThenInclude(Dp => Dp.Servicio)
            .ToListAsync();
    }
}
