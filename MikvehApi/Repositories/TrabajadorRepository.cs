using System;
using Microsoft.EntityFrameworkCore;
using MikvehApi.Data;
using MikvehApi.Models;
using MikvehApi.Repositories.Interfaces;

namespace MikvehApi.Repositories;

public class TrabajadorRepository : Repository<Trabajador>, ITrabajadorRepository
{
    public TrabajadorRepository(AppDbContext context) : base(context) { }

    public async Task<Trabajador?> GetByUserAsync(string usuario)
    {
        return await _context.Trabajadores.Include(t => t.Rol).FirstOrDefaultAsync(t => t.Usuario == usuario);
    }
}
