using McpCodeReviewServer.Models;

namespace McpCodeReviewServer.Services;

/// <summary>
/// Calculates review quality score from findings.
/// </summary>
public sealed class ReviewScorer : IReviewScorer
{
    /// <inheritdoc/>
    public int CalculateScore(IReadOnlyCollection<ReviewIssue> issues)
    {
        var score = 10;

        foreach (var issue in issues)
        {
            score -= issue.Severity switch
            {
                "critical" => 3,
                "warning" => 2,
                _ => 1
            };
        }

        return Math.Clamp(score, 0, 10);
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<CategoryReviewScore> CalculateCategoryScores(
        IReadOnlyCollection<CategoryAnalysis> categoryAnalyses,
        IReadOnlyCollection<ReviewIssue> issues)
    {
        var issueLookup = issues
            .GroupBy(issue => issue.Category, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.ToArray(), StringComparer.OrdinalIgnoreCase);

        var categoryScores = new List<CategoryReviewScore>(categoryAnalyses.Count);
        foreach (var analysis in categoryAnalyses)
        {
            var categoryIssues = issueLookup.TryGetValue(analysis.Category, out var found)
                ? found
                : Array.Empty<ReviewIssue>();

            var score = CalculateScore(categoryIssues);
            categoryScores.Add(new CategoryReviewScore(
                analysis.Category,
                score,
                analysis.RulesChecked,
                analysis.RulesMatched,
                categoryIssues.Length));
        }

        return categoryScores;
    }
}
