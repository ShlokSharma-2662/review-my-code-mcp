namespace McpCodeReviewServer.Rules.Abstractions;

/// <summary>
/// Defines a provider that yields related rules for one category area.
/// </summary>
public interface IRuleGroupProvider
{
    /// <summary>
    /// Gets the stable category name represented by this rule group.
    /// </summary>
    string Category { get; }

    /// <summary>
    /// Builds rule instances for this group.
    /// </summary>
    /// <returns>Rule instances to execute.</returns>
    IReadOnlyCollection<ICodeRule> BuildRules();
}
