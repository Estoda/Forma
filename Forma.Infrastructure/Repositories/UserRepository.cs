using Forma.Domain.Entities;
using Forma.Domain.Interfaces;
using Forma.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Forma.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FormaDbContext _context;

    public UserRepository(FormaDbContext context) => _context = context;

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == normalizedEmail);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();
        return await _context.Users
            .AnyAsync(u => u.Email == normalizedEmail);
    }

    public async Task AddAsync(User user)
    {
        user.Email = user.Email.Trim().ToLowerInvariant();
        await _context.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
