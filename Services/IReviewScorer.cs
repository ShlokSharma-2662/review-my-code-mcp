using McpCodeReviewServer.Models;

namespace McpCodeReviewServer.Services;

/// <summary>
/// Defines review scoring behavior.
/// </summary>
public interface IReviewScorer
{
    /// <summary>
    /// Calculates a score for findings.
    /// </summary>
    /// <param name="issues">Review findings.</param>
    /// <returns>Score in range 0-10.</returns>
    int CalculateScore(IReadOnlyCollection<ReviewIssue> issues);

    /// <summary>
    /// Calculates per-category scores based on matched issues and rule coverage.
    /// </summary>
    /// <param name="categoryAnalyses">Category coverage data.</param>
    /// <param name="issues">All matched issues.</param>
    /// <returns>Per-category score list.</returns>
    IReadOnlyCollection<CategoryReviewScore> CalculateCategoryScores(
        IReadOnlyCollection<CategoryAnalysis> categoryAnalyses,
        IReadOnlyCollection<ReviewIssue> issues);
}
