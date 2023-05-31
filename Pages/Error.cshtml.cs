// ReSharper disable NotAccessedField.Local

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Noted.Pages;

/// <summary>
/// Error page model
/// </summary>
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorModel : PageModel {
    private readonly ILogger<ErrorModel> _logger;

    /// <summary>
    /// Error page model constructor
    /// </summary>
    /// <param name="logger"></param>
    public ErrorModel(ILogger<ErrorModel> logger) {
        _logger = logger;
    }

    /// <summary>
    /// The request id
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether to show the request id
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    /// <summary>
    /// Get the request id
    /// </summary>
    public void OnGet() {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}