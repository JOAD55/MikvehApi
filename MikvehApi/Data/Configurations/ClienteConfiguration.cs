using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MikvehApi.Models;

namespace MikvehApi.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clientes");

        builder.HasKey(c => c.ClienteId);
        builder.Property(c => c.ClienteId)
            .HasColumnName("cliente_id");

        builder.Property(c => c.Nombre)
            .HasMaxLength(255)
            .HasColumnName("nombre");

        builder.Property(c => c.Apellidos)
            .HasMaxLength(255)
            .HasColumnName("apellidos");

        builder.Property(c => c.Telefono)
            .IsUnicode(false)
            .HasMaxLength(20)
            .HasColumnName("telefono");

        builder.Property(c => c.Email)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("email");

        builder.Property(c => c.FechaNacimiento)
            .HasColumnType("date")
            .HasColumnName("fecha_nacimiento");

        builder.HasIndex(c => new {c.Nombre, c.Apellidos});
    }
}
