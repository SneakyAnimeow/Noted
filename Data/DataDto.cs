// ReSharper disable MemberCanBePrivate.Global

namespace Noted.Data; 

/// <summary>
/// Abstract class for data transfer objects (DTOs)
/// </summary>
public abstract class DataDto {
    /// <summary>
    /// User DTO
    /// </summary>
    /// <param name="Username"> Username </param>
    /// <param name="Email"> Email </param>
    /// <param name="CreationDate"> Creation date </param>
    public record UserDto(string Username, string Email, DateTime CreationDate);
    
    /// <summary>
    /// Category DTO
    /// </summary>
    /// <param name="Id"> Id </param>
    /// <param name="Name"> Name </param>
    public record CategoryDto(long Id, string Name);
    
    /// <summary>
    /// Note DTO
    /// </summary>
    /// <param name="Id"> Id </param>
    /// <param name="Name"> Name </param>
    /// <param name="Content"> Content </param>
    public record NoteDto(long Id, string Name, string Content);
    
    /// <summary>
    /// Token DTO
    /// </summary>
    /// <param name="Token"> Token </param>
    public record TokenDto(string Token);
    
    
    /// <summary>
    /// Converts a user entity to a user DTO
    /// </summary>
    /// <param name="user"> User entity </param>
    /// <returns> User DTO </returns>
    public static UserDto ToDto(User user) => new(user.Name, user.Email, user.CreationDate);
    
    /// <summary>
    /// Converts a category entity to a category DTO
    /// </summary>
    /// <param name="category"> Category entity </param>
    /// <returns> Category DTO </returns>
    public static CategoryDto ToDto(Category category) => new(category.Id, category.Name);
    
    /// <summary>
    /// Converts a note entity to a note DTO
    /// </summary>
    /// <param name="note"> Note entity </param>
    /// <returns> Note DTO </returns>
    public static NoteDto ToDto(Note note) => new(note.Id, note.Name, note.Content);
    
    /// <summary>
    /// Converts a token entity to a token DTO
    /// </summary>
    /// <param name="token"> Token entity </param>
    /// <returns> Token DTO </returns>
    public static TokenDto ToDto(Token token) => new(token.Token1);
    
    
    /// <summary>
    /// Converts a user DTO to a user entity
    /// </summary>
    /// <param name="user"> User DTO </param>
    /// <returns> User entity </returns>
    public static User ToEntity(UserDto user) => new() {
        Name = user.Username,
        Email = user.Email,
        CreationDate = user.CreationDate
    };
    
    /// <summary>
    /// Converts a category DTO to a category entity
    /// </summary>
    /// <param name="category"> Category DTO </param>
    /// <returns> Category entity </returns>
    public static Category ToEntity(CategoryDto category) => new() {
        Id = category.Id,
        Name = category.Name
    };
    
    /// <summary>
    /// Converts a note DTO to a note entity
    /// </summary>
    /// <param name="note"> Note DTO </param>
    /// <returns> Note entity </returns>
    public static Note ToEntity(NoteDto note) => new() {
        Id = note.Id,
        Name = note.Name,
        Content = note.Content
    };
    
    /// <summary>
    /// Converts a token DTO to a token entity
    /// </summary>
    /// <param name="token"> Token DTO </param>
    /// <returns> Token entity </returns>
    public static Token ToEntity(TokenDto token) => new() {
        Token1 = token.Token,
    };
    
    
    /// <summary>
    /// Converts a collection of user entities to a collection of user DTOs
    /// </summary>
    /// <param name="users"> User entities </param>
    /// <returns> User DTOs </returns>
    public static IEnumerable<UserDto> ToDto(IEnumerable<User> users) => users.Select(ToDto);
    
    /// <summary>
    /// Converts a collection of category entities to a collection of category DTOs
    /// </summary>
    /// <param name="categories"> Category entities </param>
    /// <returns> Category DTOs </returns>
    public static IEnumerable<CategoryDto> ToDto(IEnumerable<Category> categories) => categories.Select(ToDto);
    
    /// <summary>
    /// Converts a collection of note entities to a collection of note DTOs
    /// </summary>
    /// <param name="notes"> Note entities </param>
    /// <returns> Note DTOs </returns>
    public static IEnumerable<NoteDto> ToDto(IEnumerable<Note> notes) => notes.Select(ToDto);
    
    /// <summary>
    /// Converts a collection of token entities to a collection of token DTOs
    /// </summary>
    /// <param name="tokens"> Token entities </param>
    /// <returns> Token DTOs </returns>
    public static IEnumerable<TokenDto> ToDto(IEnumerable<Token> tokens) => tokens.Select(ToDto);
    
    /// <summary>
    /// Converts a collection of user DTOs to a collection of user entities
    /// </summary>
    /// <param name="users"> User DTOs </param>
    /// <returns> User entities </returns>
    public static IEnumerable<User> ToEntity(IEnumerable<UserDto> users) => users.Select(ToEntity);
    
    /// <summary>
    /// Converts a collection of category DTOs to a collection of category entities
    /// </summary>
    /// <param name="categories"> Category DTOs </param>
    /// <returns> Category entities </returns>
    public static IEnumerable<Category> ToEntity(IEnumerable<CategoryDto> categories) => categories.Select(ToEntity);
    
    /// <summary>
    /// Converts a collection of note DTOs to a collection of note entities
    /// </summary>
    /// <param name="notes"> Note DTOs </param>
    /// <returns> Note entities </returns>
    public static IEnumerable<Note> ToEntity(IEnumerable<NoteDto> notes) => notes.Select(ToEntity);

    /// <summary>
    /// Converts a collection of token DTOs to a collection of token entities
    /// </summary>
    /// <param name="tokens"> Token DTOs </param>
    /// <returns> Token entities </returns>
    public static IEnumerable<Token> ToEntity(IEnumerable<TokenDto> tokens) => tokens.Select(ToEntity);
}