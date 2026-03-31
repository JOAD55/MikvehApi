using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MikvehApi.Models;

namespace MikvehApi.Data.Configurations;

public class DetalleCitaConfiguration : IEntityTypeConfiguration<DetalleCita>
{
    public void Configure(EntityTypeBuilder<DetalleCita> builder)
    {
        builder.ToTable("detalles_cita");

        builder.HasKey(dc => dc.DetalleCitaId);
        builder.Property(dc => dc.DetalleCitaId)
            .HasColumnName("detalle_cita_id");

        builder.Property(dc => dc.CitaId)
            .HasColumnName("cita_id");

        builder.Property(dc => dc.ServicioId)
            .HasColumnName("servicio_id");

        builder.Property(dc => dc.PaqueteId)
            .HasColumnName("paquete_id");

        builder.Property(dc => dc.Cantidad)
            .HasColumnName("cantidad");

        builder.Property(dc => dc.Subtotal)
            .HasColumnType("decimal(10,2)")
            .HasColumnName("sutotal");

        builder.HasOne(dc => dc.Cita)
            .WithMany(c => c.DetallesCita)
            .HasForeignKey(dc => dc.CitaId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_detalles_cita_cita");
        
        builder.HasOne(dc => dc.Servicio)
            .WithMany(s => s.DetallesCita)
            .HasForeignKey(dc => dc.ServicioId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_detalles_cita_servicio");

        builder.HasOne(dc => dc.Paquete)
            .WithMany(p => p.DetallesCita)
            .HasForeignKey(dc => dc.PaqueteId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_detalles_cita_paquete");
    }
}
