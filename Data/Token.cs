namespace Noted.Data;

/// <summary>
/// Token entity
/// </summary>
public partial class Token {
    /// <summary>
    /// Id of the token
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// User ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Token
    /// </summary>
    public string Token1 { get; set; } = null!;

    /// <summary>
    /// Expiration date
    /// </summary>
    public DateTime ExpireDate { get; set; }

    /// <summary>
    /// User that owns this token
    /// </summary>
    public virtual User User { get; set; } = null!;
}