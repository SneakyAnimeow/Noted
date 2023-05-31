using Noted.Data;

namespace Noted.Repositories;

/// <summary>
/// Repository interface
/// </summary>
/// <typeparam name="T"> The entity type </typeparam>
public interface IRepository<T> {
    /// <summary>
    /// Get an entity by id
    /// </summary>
    /// <param name="id"> The id of the entity to get </param>
    /// <returns> The entity </returns>
    public Task<T?> GetAsync(long id);
    
    /// <summary>
    /// Get all entities
    /// </summary>
    /// <returns> All entities </returns>
    public Task<IEnumerable<T>> GetAllAsync();
    
    /// <summary>
    /// Add an entity
    /// </summary>
    /// <param name="entity"> The entity to add </param>
    /// <returns> The added entity </returns>
    public Task<T> AddAsync(T entity);
    
    /// <summary>
    /// Update an entity
    /// </summary>
    /// <param name="entity"> The entity to update </param>
    /// <returns> The updated entity </returns>
    public Task<T> UpdateAsync(T entity);
    
    /// <summary>
    /// Delete an entity by id
    /// </summary>
    /// <param name="id"> The id of the entity to delete </param>
    /// <returns> The deleted entity </returns>
    public Task<T> DeleteAsync(long id);
    
    /// <summary>
    /// Delete an entity
    /// </summary>
    /// <param name="entity"> The entity to delete </param>
    /// <returns> The deleted entity </returns>
    public Task<T> DeleteAsync(T entity);
    
    /// <summary>
    /// Set the context for the repository
    /// </summary>
    /// <param name="context"> The context </param>
    public void SetContext(NotedContext context);
}