using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class TrabajadorRepository : Repository<Trabajador>, ITrabajadorRepository
{
    public TrabajadorRepository(AppDbContext context, IMapper mapper) : base(context, mapper) { }

    public override async Task<IEnumerable<Trabajador>> GetAllAsync()
    {
        return await _context.Trabajadores.Include(t => t.Rol).ToListAsync();
    }

    public async Task<Trabajador?> GetByUserAsync(string usuario)
    {
        return await _context.Trabajadores.Include(t => t.Rol).FirstOrDefaultAsync(t => t.Usuario == usuario);
    }
}
