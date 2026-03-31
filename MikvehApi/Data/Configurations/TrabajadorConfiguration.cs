using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MikvehApi.Models;

namespace MikvehApi.Data.Configurations;

public class TrabajadorConfiguration : IEntityTypeConfiguration<Trabajador>
{
    public void Configure(EntityTypeBuilder<Trabajador> builder)
    {
        builder.ToTable("trabajadores");

        builder.HasKey(t => t.TrabajadorId);
        builder.Property(t => t.TrabajadorId)
            .HasColumnName("trabajador_id");

        builder.Property(t => t.Nombre)
            .HasMaxLength(255)
            .HasColumnName("nombre");

        builder.Property(t => t.Apellidos)
            .HasMaxLength(255)
            .HasColumnName("apellidos");

        builder.Property(t => t.Usuario)
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasColumnName("usuario");

        builder.Property(t => t.Contrasena)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("contrasena_hash");

        builder.Property(t => t.Telefono)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("telefono");

        builder.Property(t => t.Email)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("email");

        builder.Property(t => t.FechaNacimiento)
            .HasColumnType("date")
            .HasColumnName("fecha_nacimiento");

        builder.Property(t => t.RolId)
            .HasColumnName("rol_id");

        builder.HasOne(t => t.Rol)
            .WithMany(r => r.Trabajadores)
            .HasForeignKey(t => t.RolId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_trabajadores_rol");
    }
}
