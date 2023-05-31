using Noted.Data;

namespace Noted.Repositories; 

/// <summary>
/// Category repository
/// </summary>
public interface ICategoryRepository : IRepository<Category> {
    /// <summary>
    /// Get all categories for a user
    /// </summary>
    /// <param name="userId"> The id of the user </param>
    /// <returns> All categories for the user </returns>
    public Task<IEnumerable<Category>> GetByUserIdAsync(long userId);
}