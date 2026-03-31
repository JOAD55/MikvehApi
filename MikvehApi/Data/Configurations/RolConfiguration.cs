using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MikvehApi.Models;

namespace MikvehApi.Data.Configurations;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.RolId);
        builder.Property(r => r.RolId)  
            .HasColumnName("rol_id");

        builder.Property(r => r.Nombre)
            .HasMaxLength(50)
            .HasColumnName("nombre");

        builder.Property(r => r.Descripcion)
            .HasMaxLength(500)
            .HasColumnName("descripcion");
    }
}
