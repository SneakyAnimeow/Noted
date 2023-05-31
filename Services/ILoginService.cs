namespace Noted.Services; 

/// <summary>
/// Login service
/// </summary>
public interface ILoginService {
    /// <summary>
    /// Login with a username and password
    /// </summary>
    /// <param name="username"> The username </param>
    /// <param name="password"> The password </param>
    /// <returns> The token </returns>
    public Task<string> Login(string username, string password);
    
    /// <summary>
    /// Logout with a token
    /// </summary>
    /// <param name="token"> The token </param>
    public Task Logout(string token);
    
    /// <summary>
    /// Register with a username, password and email
    /// </summary>
    /// <param name="username"> The username </param>
    /// <param name="password"> The password </param>
    /// <param name="email"> The email </param>
    /// <returns> The token </returns>
    public Task<string> Register(string username, string password, string email);
    
    /// <summary>
    /// Check if a token is still valid
    /// </summary>
    /// <param name="token"> The token </param>
    /// <returns> True if the token is still valid </returns>
    public Task<bool> TokenStillValid(string token);
}