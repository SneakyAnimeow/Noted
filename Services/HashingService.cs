using Scrypt;

namespace Noted.Services;

/// <inheritdoc />
public class HashingService : IHashingService {
    private readonly ScryptEncoder _encoder;
    private readonly string _salt;
    
    /// <summary>
    /// Hashing service constructor
    /// </summary>
    public HashingService() {
        _encoder = new ScryptEncoder();
        _salt = string.Empty;
    }
    
    /// <summary>
    /// Hashing service constructor with salt
    /// </summary>
    /// <param name="salt"> The salt </param>
    public HashingService(string salt) {
        _salt = salt;
        _encoder = new ScryptEncoder();
    }
    
    /// <inheritdoc />
    public string Hash(string password) {
        return _encoder.Encode(password+_salt);
    }

    /// <inheritdoc />
    public bool Verify(string password, string hash) {
        return _encoder.Compare(password+_salt, hash);
    }
}