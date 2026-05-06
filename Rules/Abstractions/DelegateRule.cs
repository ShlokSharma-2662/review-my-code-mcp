using McpCodeReviewServer.Models;

namespace McpCodeReviewServer.Rules.Abstractions;

/// <summary>
/// Wraps a custom delegate-based rule evaluator.
/// </summary>
public sealed class DelegateRule : ICodeRule
{
    private readonly Func<RuleContext, ReviewIssue?> _evaluator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DelegateRule"/> class.
    /// </summary>
    /// <param name="evaluator">Rule evaluation delegate.</param>
    public DelegateRule(Func<RuleContext, ReviewIssue?> evaluator)
    {
        _evaluator = evaluator;
    }

    /// <inheritdoc/>
    public ReviewIssue? Evaluate(RuleContext context) => _evaluator(context);
}
