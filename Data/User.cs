namespace Noted.Data;

/// <summary>
/// User entity
/// </summary>
public partial class User {
    /// <summary>
    /// Id of the user
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Password hash
    /// </summary>
    public string Hash { get; set; } = null!;

    /// <summary>
    /// Creation date
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Categories owned by this user
    /// </summary>
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    /// <summary>
    /// Notes owned by this user
    /// </summary>
    public virtual ICollection<Token> Tokens { get; set; } = new List<Token>();
}