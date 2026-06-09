using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VideoClub.Api.Data.Entities;

namespace VideoClub.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TipoArticulo> TiposArticulos => Set<TipoArticulo>();
    public DbSet<Genero> Generos => Set<Genero>();
    public DbSet<Idioma> Idiomas => Set<Idioma>();
    public DbSet<Articulo> Articulos => Set<Articulo>();
    public DbSet<Elenco> Elenco => Set<Elenco>();
    public DbSet<ElencoArticulo> ElencosArticulos => Set<ElencoArticulo>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Empleado> Empleados => Set<Empleado>();
    public DbSet<RolElenco> RolesElenco => Set<RolElenco>();
    public DbSet<Renta> Rentas => Set<Renta>();
    public DbSet<RentaDetalle> RentaDetalles => Set<RentaDetalle>();
    public DbSet<TipoArticuloGenero> TiposArticulosGeneros => Set<TipoArticuloGenero>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
            .Where(e => typeof(AuditableEntity).IsAssignableFrom(e.ClrType) && e.ClrType != typeof(AuditableEntity)))
        {
            var builder = modelBuilder.Entity(entityType.ClrType);
            builder.Property(nameof(AuditableEntity.Estado)).HasDefaultValue(true);
            builder.Property(nameof(AuditableEntity.FechaCreacion)).HasColumnName("fecha_creacion").HasColumnType("timestamptz").HasDefaultValueSql("now()");
            builder.Property(nameof(AuditableEntity.FechaModificacion)).HasColumnName("fecha_modificacion").HasColumnType("timestamptz");
            builder.Property(nameof(AuditableEntity.UsuarioCreacion)).HasColumnName("usuario_creacion").HasColumnType("text").IsRequired();
            builder.Property(nameof(AuditableEntity.UsuarioModificacion)).HasColumnName("usuario_modificacion").HasColumnType("text");
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Estado = true;
                    entry.Entity.FechaCreacion = DateTime.UtcNow;
                    entry.Entity.UsuarioCreacion = "system";
                    break;

                case EntityState.Modified:
                    entry.Entity.FechaModificacion = DateTime.UtcNow;
                    entry.Entity.UsuarioModificacion = "system";
                    break;
            }
        }

        return base.SaveChangesAsync(ct);
    }
}
