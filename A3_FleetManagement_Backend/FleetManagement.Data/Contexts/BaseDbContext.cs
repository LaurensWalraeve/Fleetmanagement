using FleetManagement.Data.Contexts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Data.Contexts;

public class BaseDbContext : DbContext, IBaseDbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        if (!ChangeTracker.HasChanges()) return Task.FromResult(0);

        HandleAuditableChanges();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void HandleAuditableChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var auditableEntity in ChangeTracker.Entries<Auditable>())
        {
            if (auditableEntity.State == EntityState.Added) auditableEntity.Entity.CreatedAt = now;
            auditableEntity.Entity.UpdatedAt = now;
        }
    }
}

public class Auditable
{
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}