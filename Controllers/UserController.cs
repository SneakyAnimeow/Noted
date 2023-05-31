// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Mvc;
using Noted.Services;

namespace Noted.Controllers; 

/// <summary>
/// User controller
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class UserController {
    private readonly IDataService _dataService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dataService"> The data service from dependency injection </param>
    public UserController(IDataService dataService) {
        _dataService = dataService;
    }
    
    /// <summary>
    /// Get user info by token body
    /// </summary>
    /// <param name="Token"> The token </param>
    public record GetInfoBody(string Token);
    /// <summary>
    /// Get user info by token
    /// </summary>
    /// <param name="body"> The token </param>
    /// <returns> The user info </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<UserDto?> GetInfo([FromBody] GetInfoBody body) {
        return await _dataService.GetUserAsyncByToken(body.Token);
    }
    
    /// <summary>
    /// Create user body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="Username"> The username </param>
    /// <param name="Email"> The email </param>
    /// <param name="Password"> (Optional) The password </param>
    public record UpdateUserBody(string Token, string Username, string Email, string? Password);
    /// <summary>
    /// Create user
    /// </summary>
    /// <param name="body"> The token, username, email, and password </param>
    /// <returns> The user </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<UserDto> UpdateUser([FromBody] UpdateUserBody body) {
        return await _dataService.UpdateUserAsyncByToken(body);
    }
}