using McpCodeReviewServer.Models;

namespace McpCodeReviewServer.Rules.Abstractions;

/// <summary>
/// Defines a single review rule that can emit at most one issue.
/// </summary>
public interface ICodeRule
{
    /// <summary>
    /// Evaluates this rule against the supplied context.
    /// </summary>
    /// <param name="context">Source context for evaluation.</param>
    /// <returns>A finding when the rule matches; otherwise null.</returns>
    ReviewIssue? Evaluate(RuleContext context);
}
