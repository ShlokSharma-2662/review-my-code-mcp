namespace McpCodeReviewServer.Models;

/// <summary>
/// Represents the complete review response.
/// </summary>
public sealed record ReviewResult(
    string Summary,
    int Score,
    IReadOnlyCollection<ReviewIssue> Issues,
    string InvocationId,
    int TotalRulesChecked,
    int TotalRulesMatched,
    IReadOnlyCollection<string> CheckedCategories,
    IReadOnlyCollection<CategoryReviewScore> CategoryScores,
    IReadOnlyCollection<SuggestedChange> SuggestedChanges);
