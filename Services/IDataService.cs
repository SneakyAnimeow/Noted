using UpdateUserBody = Noted.Controllers.UserController.UpdateUserBody;

namespace Noted.Services;

/// <summary>
/// Data service
/// </summary>
public interface IDataService {
    /// <summary>
    /// Get all categories
    /// </summary>
    /// <param name="token"> The token </param>
    /// <returns> The categories </returns>
    public Task<IEnumerable<CategoryDto>> GetCategoriesAsyncByToken(string token);

    /// <summary>
    /// Get a category by id
    /// </summary>
    /// <param name="token"> The token </param>
    /// <param name="id"> The id </param>
    /// <returns> The category </returns>
    public Task<CategoryDto?> GetCategoryAsyncByTokenAndId(string token, long id);

    /// <summary>
    /// Add a category
    /// </summary>
    /// <param name="token"> The token </param>
    /// <param name="entity"> The category </param>
    /// <returns> The category </returns>
    public Task<CategoryDto> AddCategoryAsyncByToken(string token, CategoryDto entity);

    /// <summary>
    /// Update a category
    /// </summary>
    /// <param name="token"> The token </param>
    /// <param name="id"> The id </param>
    /// <param name="entity"> The category </param>
    /// <returns> The category </returns>
    public Task<CategoryDto> UpdateCategoryAsyncByTokenAndId(string token, long id, CategoryDto entity);

    /// <summary>
    /// Delete a category
    /// </summary>
    /// <param name="token"> The token </param>
    /// <param name="id"> The id </param>
    /// <returns> The category </returns>
    public Task DeleteCategoryAsyncByTokenAndId(string token, long id);


    /// <summary>
    /// Get all notes
    /// </summary>
    /// <param name="token"> The token </param>
    /// <param name="categoryId"> The category id </param>
    /// <returns> The notes </returns>
    public Task<IEnumerable<NoteDto>> GetNotesAsyncByTokenAndCategoryId(string token, long categoryId);

    /// <summary>
    /// Get a note by id
    /// </summary>
    /// <param name="token"> The token </param>
    /// <param name="id"> The id </param>
    /// <returns> The note </returns>
    public Task<NoteDto?> GetNoteAsyncByTokenAndId(string token, long id);

    /// <summary>
    /// Add a note
    /// </summary>
    /// <param name="token"> The token </param>
    /// <param name="categoryId"> The category id </param>
    /// <param name="entity"> The note </param>
    /// <returns> The note </returns>
    public Task<NoteDto> AddNoteAsyncByTokenAndCategoryId(string token, long categoryId, NoteDto entity);

    /// <summary>
    /// Update a note
    /// </summary>
    /// <param name="token"> The token </param>
    /// <param name="id"> The id </param>
    /// <param name="entity"> The note </param>
    /// <returns> The note </returns>
    public Task<NoteDto> UpdateNoteAsyncByTokenAndId(string token, long id, NoteDto entity);

    /// <summary>
    /// Delete a note
    /// </summary>
    /// <param name="token"> The token </param>
    /// <param name="id"> The id </param>
    /// <returns> The note </returns>
    public Task DeleteNoteAsyncByTokenAndId(string token, long id);


    /// <summary>
    /// Get a user by token
    /// </summary>
    /// <param name="token"> The token </param>
    /// <returns> The user </returns>
    public Task<UserDto?> GetUserAsyncByToken(string token);
    
    /// <summary>
    /// Update a user by token
    /// </summary>
    /// <param name="body"> The user </param>
    /// <returns> The user </returns>
    public Task<UserDto> UpdateUserAsyncByToken(UpdateUserBody body);
}