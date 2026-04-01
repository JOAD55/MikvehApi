using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MikvehApi.Models;

namespace MikvehApi.Data.Configurations;

public class CitaConfiguration : IEntityTypeConfiguration<Cita>
{
    public void Configure(EntityTypeBuilder<Cita> builder)
    {
        builder.ToTable("citas");

        builder.HasKey(c => c.CitaId);
        builder.Property(c => c.CitaId)
            .HasColumnName("cita_id");

        builder.Property(c => c.FechaHoraCita)
            .HasColumnType("datetime")
            .HasColumnName("nombre_campo");

        builder.Property(c => c.TotalPagar)
            .HasColumnType("decimal(10,2)")
            .HasColumnName("total_pagar");

        builder.Property(c => c.ClienteId)
            .HasColumnName("cliente_id");

        builder.Property(c => c.TrabajadorId)
            .HasColumnName("trabajador_id");

        builder.HasOne(c => c.Cliente)
            .WithMany(cl => cl.Citas)
            .HasForeignKey(c => c.ClienteId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_citas_cliente");

        builder.HasOne(c => c.Trabajador)
            .WithMany(t => t.Citas)
            .HasForeignKey(c => c.TrabajadorId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_citas_trabajador");

        builder.HasIndex(c => c.FechaHoraCita);

        builder.HasIndex(c => new {c.ClienteId, c.FechaHoraCita});

        builder.HasIndex(c => new {c.TrabajadorId, c.FechaHoraCita});
    }
}
