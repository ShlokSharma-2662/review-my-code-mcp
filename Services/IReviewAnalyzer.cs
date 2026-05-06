using McpCodeReviewServer.Models;

namespace McpCodeReviewServer.Services;

/// <summary>
/// Defines source code analysis behavior for review findings.
/// </summary>
public interface IReviewAnalyzer
{
    /// <summary>
    /// Evaluates source code and returns findings.
    /// </summary>
    /// <param name="code">Source code text.</param>
    /// <param name="maxIssues">Maximum findings to return.</param>
    /// <returns>Findings list.</returns>
    ReviewAnalysisResult Analyze(string code, int maxIssues);
}
