using CachingWithDecoratorPattern.Domain;
using Microsoft.Extensions.Caching.Memory;

namespace CachingWithDecoratorPattern.Repositories;

public class CachedUserRepository : IUserRepository
{
    private readonly IUserRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    public CachedUserRepository(IUserRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var key = $"USER-{id}";

        return await _memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

            return _decorated.GetById(id, cancellationToken);
        });
    }

    public Task Add(User user, CancellationToken cancellationToken = default)
    {
        return _decorated.Add(user, cancellationToken);
    }
}