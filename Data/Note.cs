namespace Noted.Data;

/// <summary>
/// Note entity
/// </summary>
public partial class Note {
    /// <summary>
    /// Id of the note
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Note name
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Note content
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Category ID
    /// </summary>
    public long CategoryId { get; set; }

    /// <summary>
    /// Category that owns this note
    /// </summary>
    public virtual Category Category { get; set; } = null!;
}