using Noted.Data;

namespace Noted.Repositories; 

/// <summary>
/// User repository
/// </summary>
public interface IUserRepository : IRepository<User> {
    /// <summary>
    /// Get a user by their email
    /// </summary>
    /// <param name="email"> The email of the user </param>
    /// <returns> The user </returns>
    public Task<User?> GetByEmailAsync(string email);
    
    /// <summary>
    /// Get a user by their username
    /// </summary>
    /// <param name="username"> The username of the user </param>
    /// <returns> The user </returns>
    public Task<User?> GetByUsernameAsync(string username);
}