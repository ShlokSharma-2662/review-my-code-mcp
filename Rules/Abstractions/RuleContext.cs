namespace McpCodeReviewServer.Rules.Abstractions;

/// <summary>
/// Provides source context for evaluating a rule.
/// </summary>
public sealed class RuleContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RuleContext"/> class.
    /// </summary>
    /// <param name="code">Raw source code text.</param>
    /// <param name="lines">Normalized source lines.</param>
    public RuleContext(string code, IReadOnlyList<string> lines)
    {
        Code = code;
        Lines = lines;
    }

    /// <summary>
    /// Gets the full source code text.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the normalized source lines.
    /// </summary>
    public IReadOnlyList<string> Lines { get; }
}
