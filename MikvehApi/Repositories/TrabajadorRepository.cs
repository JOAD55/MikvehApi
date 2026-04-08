using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class TrabajadorRepository : Repository<Trabajador>, ITrabajadorRepository
{
    public TrabajadorRepository(AppDbContext context) : base(context) { }

    public async Task<bool> ExistAsync(string usuario)
    {
        var trabajador = await _context.Trabajadores.FirstOrDefaultAsync(t => t.Usuario == usuario);

        return trabajador is not null;
    }

    public override async Task<IEnumerable<Trabajador>> GetAllAsync()
    {
        return await _context.Trabajadores.Include(t => t.Rol).ToListAsync();
    }

    public override async Task<Trabajador?> GetByIdAsync(int id)
    {
        return await _context.Trabajadores.Include(t => t.Rol).FirstOrDefaultAsync(t => t.TrabajadorId == id);
    }

    public async Task<Trabajador?> GetByUserAsync(string usuario)
    {
        return await _context.Trabajadores.Include(t => t.Rol).FirstOrDefaultAsync(t => t.Usuario == usuario);
    }
}
