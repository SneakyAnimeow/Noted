using Noted.Data;

namespace Noted.Repositories; 

/// <summary>
/// Note repository
/// </summary>
public interface INoteRepository : IRepository<Note> {
    /// <summary>
    /// Get all notes for a user
    /// </summary>
    /// <param name="categoryId"> The id of the category </param>
    /// <returns> All notes for the user </returns>
    public Task<IEnumerable<Note>> GetByCategoryIdAsync(long categoryId);
}