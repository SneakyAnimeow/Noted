using Microsoft.EntityFrameworkCore;
using Noted.Data;

namespace Noted.Repositories;

/// <inheritdoc />
public class CategoryRepository : ICategoryRepository {
    private NotedContext _context;
    
    /// <summary>
    /// Category repository constructor
    /// </summary>
    /// <param name="context"> The database context from dependency injection </param>
    public CategoryRepository(NotedContext context) {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Category?> GetAsync(long id) {
        return await _context.Categories.Include(c => c.Notes).FirstOrDefaultAsync(c => c.Id == id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetAllAsync() {
        return await _context.Categories.Include(c => c.Notes).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Category> AddAsync(Category entity) {
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<Category> UpdateAsync(Category entity) {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    /// <summary>
    /// Delete a category by id
    /// </summary>
    /// <param name="id"> The id of the category to delete </param>
    /// <returns> The deleted category </returns>
    /// <exception cref="Exception"> Category not found </exception>
    public async Task<Category> DeleteAsync(long id) {
        var entity = await _context.Categories.FindAsync(id);
        if (entity == null) throw new Exception("Category not found");
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<Category> DeleteAsync(Category entity) {
        _context.Categories.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public void SetContext(NotedContext context) {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetByUserIdAsync(long userId) {
        return await _context.Categories.Where(c => c.UserId == userId).ToListAsync();
    }
}