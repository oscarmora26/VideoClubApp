using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoClub.Api.Data.Entities;

namespace VideoClub.Api.Data.Configurations;

public class RentaDetalleConfiguration : IEntityTypeConfiguration<RentaDetalle>
{
    public void Configure(EntityTypeBuilder<RentaDetalle> builder)
    {
        builder.ToTable("RentaDetalles", t =>
        {
            t.HasCheckConstraint("CK_RentaDetalles_CantidadDias", "\"CantidadDias\" > 0");
            t.HasCheckConstraint("CK_RentaDetalles_MontoPorDia", "\"MontoPorDia\" > 0");
            t.HasCheckConstraint("CK_RentaDetalles_DiasRetraso", "\"DiasRetraso\" >= 0");
            t.HasCheckConstraint("CK_RentaDetalles_MontoRetraso", "\"MontoRetraso\" >= 0.0");
        });

        builder.HasKey(e => e.Id);

        builder.Property(e => e.MontoPorDia).HasColumnType("numeric(10,2)").IsRequired();
        builder.Property(e => e.CantidadDias).IsRequired();
        builder.Property(e => e.MontoTotal).HasColumnType("numeric(10,2)")
            .HasComputedColumnSql("\"MontoPorDia\" * \"CantidadDias\"", stored: true);
        builder.Property(e => e.FechaDevolucionEsperada).HasColumnType("timestamptz");
        builder.Property(e => e.FechaDevolucionReal).HasColumnType("timestamptz");
        builder.Property(e => e.DiasRetraso).HasDefaultValue(0);
        builder.Property(e => e.MontoRetraso).HasColumnType("numeric(10,2)").HasDefaultValue(0.0m);
        builder.Property(e => e.Comentario).HasColumnType("text");

        builder.HasOne(e => e.Renta)
            .WithMany(r => r.Detalles)
            .HasForeignKey(e => e.RentaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Articulo)
            .WithMany(a => a.RentaDetalles)
            .HasForeignKey(e => e.ArticuloId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.RentaId);
        builder.HasIndex(e => e.ArticuloId);
    }
}
