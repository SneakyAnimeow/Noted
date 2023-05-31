// ReSharper disable ClassNeverInstantiated.Global

using Microsoft.AspNetCore.Mvc;
using Noted.Services;

namespace Noted.Controllers; 

/// <summary>
/// Note controller
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class NoteController {
    private readonly IDataService _dataService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dataService"> The data service from dependency injection </param>
    public NoteController(IDataService dataService) {
        _dataService = dataService;
    }
    
    /// <summary>
    /// Get notes by token and category id body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="CategoryId"> The category id </param>
    public record GetNotesBody(string Token, long CategoryId);
    /// <summary>
    /// Get notes by token and category id
    /// </summary>
    /// <param name="body"> The token and category id </param>
    /// <returns> The notes </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<NoteDto>> GetNotes([FromBody] GetNotesBody body) {
        return await _dataService.GetNotesAsyncByTokenAndCategoryId(body.Token, body.CategoryId);
    }
    
    /// <summary>
    /// Get note by token and id body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="Id"> The id </param>
    public record GetNoteBody(string Token, long Id);
    /// <summary>
    /// Get note by token and id
    /// </summary>
    /// <param name="body"> The token and id </param>
    /// <returns> The note </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<NoteDto?> GetNote([FromBody] GetNoteBody body) {
        return await _dataService.GetNoteAsyncByTokenAndId(body.Token, body.Id);
    }
    
    /// <summary>
    /// Create note by token and category id body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="CategoryId"> The category id </param>
    /// <param name="Title"> The title </param>
    /// <param name="Content"> The content </param>
    public record CreateNoteBody(string Token, long CategoryId, string Title, string Content);
    /// <summary>
    /// Create note by token and category id
    /// </summary>
    /// <param name="body"> The token, category id and note </param>
    /// <returns> The note </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<NoteDto> CreateNote([FromBody] CreateNoteBody body) {
        return await _dataService.AddNoteAsyncByTokenAndCategoryId(body.Token, body.CategoryId, new NoteDto(0, body.Title, body.Content));
    }
    
    /// <summary>
    /// Update note by token and id body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="Id"> The id </param>
    /// <param name="Title"> The title </param>
    /// <param name="Content"> The content </param>
    public record UpdateNoteBody(string Token, long Id, string Title, string Content);
    /// <summary>
    /// Update note by token and id
    /// </summary>
    /// <param name="body"> The token, id and note </param>
    /// <returns> The note </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<NoteDto> UpdateNote([FromBody] UpdateNoteBody body) {
        return await _dataService.UpdateNoteAsyncByTokenAndId(body.Token, body.Id, new NoteDto(0, body.Title, body.Content));
    }
    
    
    /// <summary>
    /// Delete note by token and id body
    /// </summary>
    /// <param name="Token"> The token </param>
    /// <param name="Id"> The id </param>
    public record DeleteNoteBody(string Token, long Id);
    /// <summary>
    /// Delete note by token and id
    /// </summary>
    /// <param name="body"> The token and id </param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task DeleteNote([FromBody] DeleteNoteBody body) {
        await _dataService.DeleteNoteAsyncByTokenAndId(body.Token, body.Id);
    }
}