using McpCodeReviewServer.Rules.Abstractions;

namespace McpCodeReviewServer.Rules.Performance;

/// <summary>
/// Provides performance-focused rules.
/// </summary>
public sealed class PerformanceRulesProvider : IRuleGroupProvider
{
    /// <inheritdoc/>
    public string Category => "performance";

    /// <inheritdoc/>
    public IReadOnlyCollection<ICodeRule> BuildRules() =>
        new ICodeRule[]
        {
            new ContainsTokenRule(
                "ToList().Count",
                "suggestion",
                "performance",
                "Materializing collections only to count them causes unnecessary allocations.",
                "Use Count() directly, or use collection.Count when the type supports it."),
            new ContainsTokenRule(
                ".Count() > 0",
                "suggestion",
                "performance",
                "Count() > 0 can scan full sequences for some enumerables.",
                "Use Any() to short-circuit efficiently."),
            new ContainsTokenRule(
                "new string(",
                "suggestion",
                "performance",
                "Frequent string reconstruction can create avoidable allocations.",
                "Consider StringBuilder for repeated concatenation in loops.")
        };
}
