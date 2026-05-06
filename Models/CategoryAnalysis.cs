namespace McpCodeReviewServer.Models;

/// <summary>
/// Represents rule execution coverage for a category.
/// </summary>
public sealed record CategoryAnalysis(
    string Category,
    int RulesChecked,
    int RulesMatched);
