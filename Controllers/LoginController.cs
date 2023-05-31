// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Mvc;
using Noted.Services;

namespace Noted.Controllers; 

/// <summary>
/// Login controller
/// </summary>
[ApiController]
[Route("api/[action]")]
public class LoginController {
    private readonly ILoginService _loginService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="loginService"> The login service from dependency injection </param>
    public LoginController(ILoginService loginService) {
        _loginService = loginService;
    }

    /// <summary>
    /// Login with a username and password body
    /// </summary>
    /// <param name="Username"> The username </param>
    /// <param name="Password"> The password </param>
    public record LoginBody(string Username, string Password);
    /// <summary>
    /// Login with a username and password
    /// </summary>
    /// <param name="body"> Username and password </param>
    /// <returns> The token </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<string> Login([FromBody] LoginBody body) {
        return await _loginService.Login(body.Username, body.Password);
    }
    
    /// <summary>
    /// Logout with a token body
    /// </summary>
    /// <param name="Token"> The token </param>
    public record LogoutBody(string Token);
    /// <summary>
    /// Logout with a token
    /// </summary>
    /// <param name="body"> The token </param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task Logout([FromBody] LogoutBody body) {
        await _loginService.Logout(body.Token);
    }
    
    /// <summary>
    /// Register with a username, password and email body
    /// </summary>
    /// <param name="Username"> The username </param>
    /// <param name="Password"> The password </param>
    /// <param name="Email"> The email </param>
    public record RegisterBody(string Username, string Password, string Email);
    /// <summary>
    /// Register with a username, password and email
    /// </summary>
    /// <param name="body"> Username, password and email </param>
    /// <returns> The token </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<string> Register([FromBody] RegisterBody body) {
        return await _loginService.Register(body.Username, body.Password, body.Email);
    }
    
    /// <summary>
    /// Check if a token is still valid body
    /// </summary>
    /// <param name="Token"> The token </param>
    public record TokenBody(string Token);
    /// <summary>
    /// Check if a token is still valid
    /// </summary>
    /// <param name="body"> The token </param>
    /// <returns> True if the token is still valid </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<bool> TokenStillValid([FromBody] TokenBody body) {
        return await _loginService.TokenStillValid(body.Token);
    }
}