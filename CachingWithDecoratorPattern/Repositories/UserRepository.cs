using CachingWithDecoratorPattern.Domain;
using CachingWithDecoratorPattern.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CachingWithDecoratorPattern.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContext _context;

    public UserRepository(IDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task Add(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
    }
}