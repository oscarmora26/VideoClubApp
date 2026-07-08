using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoClub.Api.Data.Entities;

namespace VideoClub.Api.Data.Configurations;

public class ElencoRolConfiguration : IEntityTypeConfiguration<ElencoRol>
{
    public void Configure(EntityTypeBuilder<ElencoRol> builder)
    {
        builder.ToTable("ElencoRol");
        builder.HasKey(e => new { e.ElencoId, e.RolElencoId });

        builder.HasOne(e => e.Elenco)
            .WithMany(el => el.Roles)
            .HasForeignKey(e => e.ElencoId);

        builder.HasOne(e => e.RolElenco)
            .WithMany(r => r.Elencos)
            .HasForeignKey(e => e.RolElencoId);
    }
}
