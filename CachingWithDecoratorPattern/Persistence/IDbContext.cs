using CachingWithDecoratorPattern.Domain;
using Microsoft.EntityFrameworkCore;

namespace CachingWithDecoratorPattern.Persistence;

public interface IDbContext
{
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}