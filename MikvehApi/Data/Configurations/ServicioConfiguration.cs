using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MikvehApi.Models;

namespace MikvehApi.Data.Configurations;

public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
{
    public void Configure(EntityTypeBuilder<Servicio> builder)
    {
        builder.ToTable("servicios");

        builder.HasKey(s => s.ServicioId);
        builder.Property(s => s.ServicioId)
            .HasColumnName("servicio_id");

        builder.Property(s => s.Nombre)
            .HasMaxLength(100)
            .HasColumnName("nombre");

        builder.Property(s => s.Descripcion)
            .HasMaxLength(500)
            .HasColumnName("descripcion");

        builder.Property(s => s.DuracionMinutos)
            .HasColumnName("duracion_minutos");

        builder.Property(s => s.PrecioBase)
            .HasColumnType("decimal(10,2)")
            .HasColumnName("precio_base");
    }
}
