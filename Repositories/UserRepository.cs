using Microsoft.EntityFrameworkCore;
using Noted.Data;

namespace Noted.Repositories;

/// <inheritdoc />
public class UserRepository : IUserRepository {
    private NotedContext _context;

    /// <summary>
    /// User repository constructor
    /// </summary>
    /// <param name="context"> The database context from dependency injection </param>
    public UserRepository(NotedContext context) {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<User?> GetAsync(long id) {
        return await _context.Users.Include(u => u.Categories).ThenInclude(c => c.Notes)
            .Include(u => u.Tokens).FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetAllAsync() {
        return await _context.Users.Include(u => u.Categories).ThenInclude(c => c.Notes)
            .Include(u => u.Tokens).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<User> AddAsync(User entity) {
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<User> UpdateAsync(User entity) {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Delete a user by id
    /// </summary>
    /// <param name="id"> The id of the user to delete </param>
    /// <returns> The deleted user </returns>
    /// <exception cref="Exception"> User not found </exception>
    public async Task<User> DeleteAsync(long id) {
        var entity = await _context.Users.FindAsync(id);
        if (entity == null) throw new Exception("User not found");
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<User> DeleteAsync(User entity) {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    /// <inheritdoc />
    public void SetContext(NotedContext context) {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<User?> GetByEmailAsync(string email) {
        return await _context.Users.Include(u => u.Tokens)
            .Include(u => u.Categories).ThenInclude(c => c.Notes).FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <inheritdoc />
    public async Task<User?> GetByUsernameAsync(string username) {
        return await _context.Users.Include(u => u.Tokens)
            .Include(u => u.Categories).ThenInclude(c => c.Notes).FirstOrDefaultAsync(u => u.Name == username);
    }
}