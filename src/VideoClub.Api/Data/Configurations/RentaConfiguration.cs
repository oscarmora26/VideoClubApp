using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoClub.Api.Data.Entities;

namespace VideoClub.Api.Data.Configurations;

public class RentaConfiguration : IEntityTypeConfiguration<Renta>
{
    public void Configure(EntityTypeBuilder<Renta> builder)
    {
        builder.ToTable("Rentas", t =>
        {
            t.HasCheckConstraint("CK_Rentas_EstadoRenta", "\"EstadoRenta\" IN ('Activa', 'Devuelta')");
            t.HasCheckConstraint("CK_Rentas_FechaExpected", "\"FechaExpectedDevolucion\" > \"FechaRenta\"");
            t.HasCheckConstraint("CK_Rentas_FechaReal", "\"FechaDevolucionReal\" IS NULL OR \"FechaDevolucionReal\" >= \"FechaRenta\"");
            t.HasCheckConstraint("CK_Rentas_MontoRetraso", "\"MontoRetraso\" >= 0.0");
        });

        builder.HasKey(e => e.Id);

        builder.Property(e => e.NoRenta).HasColumnType("text").IsRequired();
        builder.Property(e => e.FechaRenta).HasColumnType("timestamptz").IsRequired().HasDefaultValueSql("now()");
        builder.Property(e => e.FechaExpectedDevolucion).HasColumnType("timestamptz");
        builder.Property(e => e.FechaDevolucionReal).HasColumnType("timestamptz");
        builder.Property(e => e.MontoTotal).HasColumnType("numeric(10,2)");
        builder.Property(e => e.MontoRetraso).HasColumnType("numeric(10,2)").HasDefaultValue(0.0m);
        builder.Property(e => e.EstadoRenta).HasColumnType("text").IsRequired().HasDefaultValue("ACTIVA");
        builder.Property(e => e.Comentario).HasColumnType("text");

        builder.HasOne(e => e.Cliente)
            .WithMany(c => c.Rentas)
            .HasForeignKey(e => e.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Empleado)
            .WithMany(em => em.Rentas)
            .HasForeignKey(e => e.EmpleadoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.NoRenta).IsUnique();
        builder.HasIndex(e => new { e.ClienteId, e.FechaRenta }).IsDescending(false, true);
        builder.HasIndex(e => new { e.EstadoRenta, e.FechaRenta }).IsDescending(false, true);
        builder.HasIndex(e => e.NoRenta);
    }
}
