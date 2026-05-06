namespace McpCodeReviewServer.Models;

/// <summary>
/// Represents per-category score and findings impact.
/// </summary>
public sealed record CategoryReviewScore(
    string Category,
    int Score,
    int RulesChecked,
    int RulesMatched,
    int IssueCount);
