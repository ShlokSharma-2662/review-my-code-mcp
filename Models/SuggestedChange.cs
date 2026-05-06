namespace McpCodeReviewServer.Models;

/// <summary>
/// Represents a normalized suggested change entry for output consumers.
/// </summary>
public sealed record SuggestedChange(
    string Severity,
    string Category,
    int? Line,
    string Problem,
    string SuggestedFix);
