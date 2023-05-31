namespace Noted.Services; 

/// <summary>
/// Hashing service
/// </summary>
public interface IHashingService {
    /// <summary>
    /// Hash a password
    /// </summary>
    /// <param name="password"> The password </param>
    /// <returns> The hash </returns>
    string Hash(string password);
    
    /// <summary>
    /// Verify a password with a hash
    /// </summary>
    /// <param name="password"> The password </param>
    /// <param name="hash"> The hash </param>
    /// <returns> True if the password matches the hash </returns>
    bool Verify(string password, string hash);
}