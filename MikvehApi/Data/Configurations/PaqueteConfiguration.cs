using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MikvehApi.Models;

namespace MikvehApi.Data.Configurations;

public class PaqueteConfiguration : IEntityTypeConfiguration<Paquete>
{
    public void Configure(EntityTypeBuilder<Paquete> builder)
    {
        builder.ToTable("paquetes");

        builder.HasKey(p => p.PaqueteId);
        builder.Property(p => p.PaqueteId)
            .HasColumnName("paquete_id");

        builder.Property(p => p.Nombre)
            .HasMaxLength(100)
            .HasColumnName("nombre");

        builder.Property(p => p.Descripcion)
            .HasMaxLength(500)
            .HasColumnName("descripcion");

        builder.Property(p => p.Precio)
            .HasColumnType("decimal(10,2)")
            .HasColumnName("precio");
    }
}
