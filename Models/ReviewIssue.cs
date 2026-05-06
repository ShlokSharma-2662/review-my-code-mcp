namespace McpCodeReviewServer.Models;

/// <summary>
/// Represents a single code review finding.
/// </summary>
public sealed record ReviewIssue(
    string Severity,
    string Category,
    int? Line,
    string Description,
    string Fix);
