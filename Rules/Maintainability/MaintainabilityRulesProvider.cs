using McpCodeReviewServer.Rules.Abstractions;

namespace McpCodeReviewServer.Rules.Maintainability;

/// <summary>
/// Provides maintainability-focused rules.
/// </summary>
public sealed class MaintainabilityRulesProvider : IRuleGroupProvider
{
    /// <inheritdoc/>
    public string Category => "maintainability";

    /// <inheritdoc/>
    public IReadOnlyCollection<ICodeRule> BuildRules() =>
        new ICodeRule[]
        {
            new ContainsTokenRule(
                "catch (Exception)",
                "suggestion",
                "maintainability",
                "Catching broad exceptions can hide actionable failures and make debugging difficult.",
                "Catch specific exception types and include contextual logging."),
            new ContainsTokenRule(
                "catch {",
                "warning",
                "correctness",
                "Empty catch block suppresses failures and may mask data corruption or partial operations.",
                "Log and handle the exception explicitly, or rethrow when appropriate.")
        };
}
