// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Mvc;
using Noted.Services;

namespace Noted.Controllers; 

/// <summary>
/// Category controller
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class CategoryController {
    private readonly IDataService _dataService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dataService"> The data service from dependency injection </param>
    public CategoryController(IDataService dataService) {
        _dataService = dataService;
    }
    
    /// <summary>
    /// Get categories by token body
    /// </summary>
    /// <param name="Token"> The token </param>
    public record GetCategoriesBody(string Token);
    /// <summary>
    /// Get categories by token
    /// </summary>
    /// <param name="body"> The token </param>
    /// <returns> The categories </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<CategoryDto>> GetCategories([FromBody] GetCategoriesBody body) {
        return await _dataService.GetCategoriesAsyncByToken(body.Token);
    }
    
    /// <summary>
    /// Get category by token and id body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="Id"> The id </param>
    public record GetCategoryBody(string Token, long Id);
    /// <summary>
    /// Get category by token and id
    /// </summary>
    /// <param name="body"> The token and id </param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CategoryDto?> GetCategory([FromBody] GetCategoryBody body) {
        return await _dataService.GetCategoryAsyncByTokenAndId(body.Token, body.Id);
    }
    
    /// <summary>
    /// Create category by token body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="Name"> The name </param>
    public record CreateCategoryBody(string Token, string Name);
    /// <summary>
    /// Create category by token
    /// </summary>
    /// <param name="body"> The token and category </param>
    /// <returns> The category </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CategoryDto> CreateCategory([FromBody] CreateCategoryBody body) {
        return await _dataService.AddCategoryAsyncByToken(body.Token, new CategoryDto(0, body.Name));
    }
    
    /// <summary>
    /// Update category by token and id body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="Id"> The id </param>
    /// <param name="Name"> The name </param>
    public record UpdateCategoryBody(string Token, long Id, string Name);
    /// <summary>
    /// Update category by token and id
    /// </summary>
    /// <param name="body"> The token, id and category </param>
    /// <returns> The category </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<CategoryDto> UpdateCategory([FromBody] UpdateCategoryBody body) {
        return await _dataService.UpdateCategoryAsyncByTokenAndId(body.Token, body.Id, new CategoryDto(0, body.Name));
    }
    
    /// <summary>
    /// Delete category by token and id body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="Id"> The id </param>
    public record DeleteCategoryBody(string Token, long Id);
    /// <summary>
    /// Delete category by token and id
    /// </summary>
    /// <param name="body"> The token and id </param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task DeleteCategory([FromBody] DeleteCategoryBody body) {
        await _dataService.DeleteCategoryAsyncByTokenAndId(body.Token, body.Id);
    }
}