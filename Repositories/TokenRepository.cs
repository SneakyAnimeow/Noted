using Microsoft.EntityFrameworkCore;
using Noted.Data;

namespace Noted.Repositories;

/// <inheritdoc />
public class TokenRepository : ITokenRepository {
    private NotedContext _context;
    
    /// <summary>
    /// Token repository constructor
    /// </summary>
    /// <param name="context"> The database context from dependency injection </param>
    public TokenRepository(NotedContext context) {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Token?> GetAsync(long id) {
        return await _context.Tokens.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Token>> GetAllAsync() {
        return await _context.Tokens.Include(t => t.User).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Token> AddAsync(Token entity) {
        _context.Tokens.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<Token> UpdateAsync(Token entity) {
        _context.Tokens.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    /// <summary>
    /// Delete a token by id
    /// </summary>
    /// <param name="id"> The id of the token to delete </param>
    /// <returns> The deleted token </returns>
    /// <exception cref="Exception"> Token not found </exception>
    public async Task<Token> DeleteAsync(long id) {
        var entity = await _context.Tokens.FindAsync(id);
        if (entity == null) throw new Exception("Token not found");
        _context.Tokens.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<Token> DeleteAsync(Token entity) {
        _context.Tokens.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    /// <inheritdoc />
    public void SetContext(NotedContext context) {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Token>> GetStillValidByUserIdAsync(long userId) {
        return await _context.Tokens.Include(t => t.User).Where(t => t.UserId == userId && t.ExpireDate > DateTime.Now).ToListAsync();
    }

    /// <inheritdoc />
    public Task<Token?> GetByTokenAsync(string token) {
        return _context.Tokens.Include(t => t.User).FirstOrDefaultAsync(t => t.Token1 == token);
    }
}