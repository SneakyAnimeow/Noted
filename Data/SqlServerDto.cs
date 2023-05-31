namespace Noted.Data; 

/// <summary>
/// Sql server DTO
/// </summary>
public class SqlServerDto {
    /// <summary>
    /// Server
    /// </summary>
    public string Server { get; set; } = null!;
    
    /// <summary>
    /// Database
    /// </summary>
    public string Database { get; set; } = null!;
    
    /// <summary>
    /// User
    /// </summary>
    public string User { get; set; } = null!;
    
    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; } = null!;
    
    /// <summary>
    /// Trusted connection
    /// </summary>
    public bool TrustedConnection { get; set; }
    
    /// <summary>
    /// Trust server certificate
    /// </summary>
    public bool TrustServerCertificate { get; set; }
    
    /// <summary>
    /// Other parameters
    /// </summary>
    public string OtherParams { get; set; } = null!;
    
    /// <summary>
    /// Converts the DTO to a connection string
    /// </summary>
    /// <returns> The connection string </returns>
    public override string ToString() {
        return $"Server={Server};Database={Database};User={User};Password={Password};Trusted_Connection={TrustedConnection};TrustServerCertificate={TrustServerCertificate};{OtherParams}";
    }
}