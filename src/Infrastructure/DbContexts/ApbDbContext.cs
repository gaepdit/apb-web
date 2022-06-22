using Apb.Domain.BaseInterfaces;
using Apb.Domain.Facilities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Apb.Infrastructure.DbContexts;

public class ApbDbContext : DbContext
{
    private readonly IHttpContextAccessor? _httpContextAccessor;

    public ApbDbContext(DbContextOptions<ApbDbContext> options, IHttpContextAccessor? accessor) : base(options) =>
        _httpContextAccessor = accessor;

    public DbSet<Facility> Facilities { get; [UsedImplicitly] set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Facility>()
            .OwnsOne(e => e.FacilityId,
                f => f.Property(e => e.FacilityId).HasColumnName("FacilityId"));
        // TODO: Make facility ID unique (Maybe use facility ID as string as primary key?)
        // modelBuilder.Entity<Facility>().HasIndex(e => e.FacilityId).IsUnique();

        base.OnModelCreating(modelBuilder);

        // Add audit properties to auditable entities
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where(e => typeof(IAuditable).IsAssignableFrom(e.ClrType)).Select(e => e.ClrType))
        {
            modelBuilder.Entity(entityType).Property<DateTimeOffset?>(AuditProperties.CreatedAt);
            modelBuilder.Entity(entityType).Property<string>(AuditProperties.CreatedBy);
            modelBuilder.Entity(entityType).Property<DateTimeOffset?>(AuditProperties.UpdatedAt);
            modelBuilder.Entity(entityType).Property<string>(AuditProperties.UpdatedBy);
        }
    }

    public override int SaveChanges()
    {
        SetAuditProperties();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        SetAuditProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditProperties()
    {
        var currentUser = _httpContextAccessor?.HttpContext?.User.Identity?.Name;

        var entries = ChangeTracker.Entries()
            .Where(e => (e.State is EntityState.Added or EntityState.Modified) && e.Entity is IAuditable);

        foreach (var entry in entries)
        {
            entry.Property(AuditProperties.UpdatedAt).CurrentValue = DateTimeOffset.Now;
            entry.Property(AuditProperties.UpdatedBy).CurrentValue = currentUser;
            if (entry.State == EntityState.Modified) continue;
            entry.Property(AuditProperties.CreatedAt).CurrentValue = DateTimeOffset.Now;
            entry.Property(AuditProperties.CreatedBy).CurrentValue = currentUser;
        }
    }

    private static class AuditProperties
    {
        internal const string CreatedAt = "CreatedAt";
        internal const string CreatedBy = "CreatedBy";
        internal const string UpdatedAt = "UpdatedAt";
        internal const string UpdatedBy = "UpdatedBy";
    }
}
