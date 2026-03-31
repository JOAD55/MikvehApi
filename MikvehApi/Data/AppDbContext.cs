using System;
using Microsoft.EntityFrameworkCore;
using MikvehApi.Models;

namespace MikvehApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Cita> Citas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<DetalleCita> DetallesCita { get; set; }
    public DbSet<DetallePaquete> DetallesPaquete { get; set; }
    public DbSet<Paquete> Paquetes { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Servicio> Servicios { get; set; }
    public DbSet<Trabajador> Trabajadores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
