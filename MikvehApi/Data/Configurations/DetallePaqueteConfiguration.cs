using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MikvehApi.Models;

namespace MikvehApi.Data.Configurations;

public class DetallePaqueteConfiguration : IEntityTypeConfiguration<DetallePaquete>
{
    public void Configure(EntityTypeBuilder<DetallePaquete> builder)
    {
        builder.ToTable("detalles_paquete");

        builder.HasKey(dp => dp.DetallePaqueteId);
        builder.Property(dp => dp.DetallePaqueteId)
            .HasColumnName("detalle_paquete_id");

        builder.Property(dp => dp.PaqueteId)
            .HasColumnName("paquete_id");

        builder.Property(dp => dp.ServicioId)
            .HasColumnName("servicio_id");

        builder.HasOne(dp => dp.Paquete)
            .WithMany(p => p.DetallesPaquete)
            .HasForeignKey(dp => dp.PaqueteId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_detalles_paquete_paquete");

        builder.HasOne(dp => dp.Servicio)
            .WithMany(s => s.DetallesPaquete)
            .HasForeignKey(dp => dp.ServicioId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_detalles_paquete_servicio");
    }
}
