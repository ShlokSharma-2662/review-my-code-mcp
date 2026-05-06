namespace McpCodeReviewServer.Models;

/// <summary>
/// Represents full analyzer output including complete and returned findings.
/// </summary>
public sealed record ReviewAnalysisResult(
    IReadOnlyCollection<ReviewIssue> AllIssues,
    IReadOnlyCollection<ReviewIssue> Issues,
    IReadOnlyCollection<CategoryAnalysis> CategoryAnalyses);
