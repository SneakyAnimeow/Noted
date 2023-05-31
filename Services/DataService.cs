using UpdateUserBody = Noted.Controllers.UserController.UpdateUserBody;
using Noted.Data;
using Noted.Extensions;
using Noted.Repositories;

namespace Noted.Services;

/// <inheritdoc />
public class DataService : IDataService {
    private readonly INoteRepository _noteRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    
    /// <summary>
    /// Constructor for DataService
    /// </summary>
    /// <param name="noteRepository"> The note repository from the DI container </param>
    /// <param name="categoryRepository"> The category repository from the DI container </param>
    /// <param name="tokenRepository"> The token repository from the DI container </param>
    /// <param name="userRepository"> The user repository from the DI container </param>
    /// <param name="hashingService"> The hashing service from the DI container </param>
    public DataService(INoteRepository noteRepository, ICategoryRepository categoryRepository, ITokenRepository tokenRepository, 
        IUserRepository userRepository, IHashingService hashingService) {
        _noteRepository = noteRepository;
        _categoryRepository = categoryRepository;
        _tokenRepository = tokenRepository;
        _userRepository = userRepository;
        _hashingService = hashingService;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsyncByToken(string token) {
        var user = await GetUserByToken(token);
        return DataDto.ToDto(await _categoryRepository.GetByUserIdAsync(user.Id));
    }

    /// <inheritdoc />
    public async Task<CategoryDto?> GetCategoryAsyncByTokenAndId(string token, long id) {
        var user = await GetUserByToken(token);
        var category = await _categoryRepository.GetAsync(id);
        
        if(category is null || category.UserId != user.Id) {
            throw new Exception("Category not found");
        }
        
        return DataDto.ToDto(category);
    }

    /// <inheritdoc />
    public async Task<CategoryDto> AddCategoryAsyncByToken(string token, CategoryDto entity) {
        var user = await GetUserByToken(token);

        var category = new Category {
            Name = entity.Name,
            UserId = user.Id
        };

        category.Name.Validate("Name", 1, 32);
        
        await _categoryRepository.AddAsync(category);
        
        return DataDto.ToDto(category);
    }

    /// <inheritdoc />
    public async Task<CategoryDto> UpdateCategoryAsyncByTokenAndId(string token, long id, CategoryDto entity) {
        var user = await GetUserByToken(token);
        
        var category = await _categoryRepository.GetAsync(id);
        
        if(category is null || category.UserId != user.Id) {
            throw new Exception("Category not found");
        }
        
        category.Name = entity.Name;
        
        category.Name.Validate("Name", 1, 32);
        
        await _categoryRepository.UpdateAsync(category);
        
        return DataDto.ToDto(category);
    }

    /// <inheritdoc />
    public async Task DeleteCategoryAsyncByTokenAndId(string token, long id) {
        var user = await GetUserByToken(token);
        
        var category = await _categoryRepository.GetAsync(id);
        
        if(category is null || category.UserId != user.Id) {
            throw new Exception("Category not found");
        }
        
        await _categoryRepository.DeleteAsync(category);
    }


    /// <inheritdoc />
    public async Task<IEnumerable<NoteDto>> GetNotesAsyncByTokenAndCategoryId(string token, long categoryId) {
        var user = await GetUserByToken(token);
        
        var category = await _categoryRepository.GetAsync(categoryId);
        
        if(category is null || category.UserId != user.Id) {
            throw new Exception("Category not found");
        }
        
        return DataDto.ToDto(await _noteRepository.GetByCategoryIdAsync(categoryId));
    }

    /// <inheritdoc />
    public async Task<NoteDto?> GetNoteAsyncByTokenAndId(string token, long id) {
        var user = await GetUserByToken(token);
        
        var note = await _noteRepository.GetAsync(id);
        
        if(note is null || note.Category.UserId != user.Id) {
            throw new Exception("Note not found");
        }
        
        return DataDto.ToDto(note);
    }

    /// <inheritdoc />
    public async Task<NoteDto> AddNoteAsyncByTokenAndCategoryId(string token, long categoryId, NoteDto entity) {
        var user = await GetUserByToken(token);
        
        var category = await _categoryRepository.GetAsync(categoryId);
        
        if(category is null || category.UserId != user.Id) {
            throw new Exception("Category not found");
        }
        
        var note = new Note {
            Name = entity.Name,
            Content = entity.Content,
            CategoryId = categoryId
        };
        
        note.Name.Validate("Name", 1, 32);
        note.Content.Validate("Content", 0, 2048);
        
        await _noteRepository.AddAsync(note);
        
        return DataDto.ToDto(note);
    }

    /// <inheritdoc />
    public async Task<NoteDto> UpdateNoteAsyncByTokenAndId(string token, long id, NoteDto entity) {
        var user = await GetUserByToken(token);
        
        var note = await _noteRepository.GetAsync(id);
        
        if(note is null || note.Category.UserId != user.Id) {
            throw new Exception("Note not found");
        }
        
        note.Name = entity.Name;
        note.Content = entity.Content;
        
        note.Name.Validate("Name", 1, 32);
        note.Content.Validate("Content", 0, 2048);
        
        await _noteRepository.UpdateAsync(note);
        
        return DataDto.ToDto(note);
    }

    /// <inheritdoc />
    public async Task DeleteNoteAsyncByTokenAndId(string token, long id) {
        var user = await GetUserByToken(token);
        
        var note = await _noteRepository.GetAsync(id);
        
        if(note is null || note.Category.UserId != user.Id) {
            throw new Exception("Note not found");
        }
        
        await _noteRepository.DeleteAsync(note);
    }


    /// <inheritdoc />
    public async Task<UserDto?> GetUserAsyncByToken(string token) {
        return DataDto.ToDto(await GetUserByToken(token));
    }

    /// <inheritdoc />
    public async Task<UserDto> UpdateUserAsyncByToken(UpdateUserBody body) {
        var user = await GetUserByToken(body.Token);

        var userWithSameEmail = await _userRepository.GetByEmailAsync(body.Email);
        if (userWithSameEmail != null && userWithSameEmail.Id != user.Id) {
            throw new Exception("Email already taken");
        }
        
        user.Email = body.Email;
        user.Email.Validate("Email", 5, 128);
        
        var userWithSameUsername = await _userRepository.GetByUsernameAsync(body.Username);
        if (userWithSameUsername != null && userWithSameUsername.Id != user.Id) {
            throw new Exception("Username already taken");
        }

        user.Name = body.Username;
        user.Name.Validate("Username", 3, 32);
        
        if (body.Password != null) {
            body.Password.Validate("Password", 3, 2048);
            user.Hash = _hashingService.Hash(body.Password);
        }
        
        await _userRepository.UpdateAsync(user);

        return DataDto.ToDto(user);
    }

    /// <summary>
    /// Get the user by token
    /// </summary>
    /// <param name="token"> The token </param>
    /// <returns> The user </returns>
    /// <exception cref="Exception"> If the token is not found or expired </exception>
    private async Task<User> GetUserByToken(string token) {
        var tokenEntity = await _tokenRepository.GetByTokenAsync(token);
        if (tokenEntity == null) {
            throw new Exception("Token not found");
        }
        if (tokenEntity.ExpireDate < DateTime.Now) {
            throw new Exception("Token expired");
        }
        return tokenEntity.User;
    }
}