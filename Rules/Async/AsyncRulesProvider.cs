using McpCodeReviewServer.Rules.Abstractions;

namespace McpCodeReviewServer.Rules.Async;

/// <summary>
/// Provides async correctness rules.
/// </summary>
public sealed class AsyncRulesProvider : IRuleGroupProvider
{
    /// <inheritdoc/>
    public string Category => "async correctness";

    /// <inheritdoc/>
    public IReadOnlyCollection<ICodeRule> BuildRules() =>
        new ICodeRule[]
        {
            new ContainsTokenRule(
                "async void ",
                "critical",
                "async correctness",
                "Avoid async void methods except event handlers; exceptions can be unobserved and crash the process.",
                "Return Task instead of async void. Example: public async Task MyMethodAsync() { ... }"),
            new ContainsTokenRule(
                ".Result",
                "warning",
                "async correctness",
                "Synchronous blocking on Task via .Result can deadlock and hurts scalability.",
                "Use await instead of .Result. Propagate async through the call chain."),
            new ContainsTokenRule(
                ".Wait(",
                "warning",
                "async correctness",
                "Blocking wait on asynchronous work can deadlock and waste thread pool threads.",
                "Use await task.ConfigureAwait(false) in library code, or await task in app code.")
        };
}
