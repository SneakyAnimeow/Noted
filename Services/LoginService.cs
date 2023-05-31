using Noted.Data;
using Noted.Extensions;
using Noted.Repositories;
using Serilog;

namespace Noted.Services;

/// <inheritdoc />
public class LoginService : ILoginService{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IHashingService _hashingService;
    
    /// <summary>
    /// Constructor for LoginService
    /// </summary>
    /// <param name="userRepository"> The user repository from the DI container </param>
    /// <param name="tokenRepository"> The token repository from the DI container </param>
    /// <param name="hashingService"> The hashing service from the DI container </param>
    public LoginService(IUserRepository userRepository, ITokenRepository tokenRepository, IHashingService hashingService) {
        _userRepository = userRepository;
        _tokenRepository = tokenRepository;
        _hashingService = hashingService;
    }

    /// <inheritdoc />
    public async Task<string> Login(string username, string password) {
        username.Validate("Username", 3, 32);

        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null) throw new Exception("User not found");
        if (!_hashingService.Verify(password, user.Hash)) throw new Exception("Invalid password");
        
        var token = new Token {
            User = user,
            Token1 = Guid.NewGuid().ToString(),
            ExpireDate = DateTime.Now.AddHours(12)
        };
        await _tokenRepository.AddAsync(token);
        Log.Information("User {UserName} logged in", user.Name);
        return token.Token1;
    }

    /// <inheritdoc />
    public async Task Logout(string token) {
        token.Validate("Token", 1, 128);

        var tokenEntity = await _tokenRepository.GetByTokenAsync(token);
        if (tokenEntity == null) throw new Exception("Token not found");
        Log.Information("User {UserName} logged out", tokenEntity.User.Name);
        await _tokenRepository.DeleteAsync(tokenEntity);
    }

    /// <inheritdoc />
    public async Task<string> Register(string username, string password, string email) {
        username.Validate("Username", 3, 32);
        email.Validate("Email", 5, 128);
        password.Validate("Password", 3, 2048);

        var user = new User {
            Name = username,
            Email = email,
            Hash = _hashingService.Hash(password),
            CreationDate = DateTime.Now
        };
        
        if (await _userRepository.GetByUsernameAsync(username) != null) throw new Exception("Username already taken");
        if (await _userRepository.GetByEmailAsync(email) != null) throw new Exception("Email already taken");
        
        await _userRepository.AddAsync(user);
        Log.Information("User {UserName} registered", user.Name);
        return await Login(username, password);
    }

    /// <inheritdoc />
    public async Task<bool> TokenStillValid(string token) {
        var tokenEntity = await _tokenRepository.GetByTokenAsync(token);
        if (tokenEntity == null) return false;
        return tokenEntity.ExpireDate > DateTime.Now;
    }
}