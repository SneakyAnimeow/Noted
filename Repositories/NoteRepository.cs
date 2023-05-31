using Microsoft.EntityFrameworkCore;
using Noted.Data;

namespace Noted.Repositories;

/// <inheritdoc />
public class NoteRepository : INoteRepository {
    private NotedContext _context;
    
    /// <summary>
    /// Note repository constructor
    /// </summary>
    /// <param name="context"> The database context from dependency injection </param>
    public NoteRepository(NotedContext context) {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<Note?> GetAsync(long id) {
        return await _context.Notes.Include(n => n.Category).FirstOrDefaultAsync(n => n.Id == id);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Note>> GetAllAsync() {
        return await _context.Notes.Include(n => n.Category).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Note> AddAsync(Note entity) {
        _context.Notes.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<Note> UpdateAsync(Note entity) {
        _context.Notes.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    /// <summary>
    /// Delete a note by id
    /// </summary>
    /// <param name="id"> The id of the note to delete </param>
    /// <returns> The deleted note </returns>
    /// <exception cref="Exception"> Note not found </exception>
    public async Task<Note> DeleteAsync(long id) {
        var entity = await _context.Notes.FindAsync(id);
        if (entity == null) throw new Exception("Note not found");
        _context.Notes.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<Note> DeleteAsync(Note entity) {
        _context.Notes.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    /// <inheritdoc />
    public void SetContext(NotedContext context) {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Note>> GetByCategoryIdAsync(long categoryId) {
        return await _context.Notes.Include(n => n.Category).Where(n => n.CategoryId == categoryId).ToListAsync();
    }
}