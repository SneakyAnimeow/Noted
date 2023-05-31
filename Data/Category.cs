namespace Noted.Data;

/// <summary>
/// Category entity
/// </summary>
public partial class Category {
    /// <summary>
    /// Id of the category
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Category name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// User ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Notes in this category
    /// </summary>
    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    /// <summary>
    /// User that owns this category
    /// </summary>
    public virtual User User { get; set; } = null!;
}