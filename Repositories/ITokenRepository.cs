using Noted.Data;

namespace Noted.Repositories; 

/// <summary>
/// Token repository
/// </summary>
public interface ITokenRepository : IRepository<Token> {
    /// <summary>
    /// Get a list of tokens by user id that are still valid
    /// </summary>
    /// <param name="userId"> The id of the user </param>
    /// <returns> A list of tokens </returns>
    public Task<IEnumerable<Token>> GetStillValidByUserIdAsync(long userId);
    
    /// <summary>
    /// Get a token by its token
    /// </summary>
    /// <param name="token"> The token </param>
    /// <returns> The token </returns>
    public Task<Token?> GetByTokenAsync(string token);
}