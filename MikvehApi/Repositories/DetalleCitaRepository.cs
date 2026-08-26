using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class DetalleCitaRepository : Repository<DetalleCita>, IDetalleCitaRepository
{
    public DetalleCitaRepository(AppDbContext context) : base(context) { }

    public override async Task<DetalleCita?> GetByIdAsync(int id)
    {
        return await _context.DetallesCita
            .Include(dc => dc.Servicio)
            .Include(dc => dc.Paquete)
            .FirstOrDefaultAsync(dc => dc.DetalleCitaId == id);
    }
}
