using McpCodeReviewServer.Models;

namespace McpCodeReviewServer.Rules.Abstractions;

/// <summary>
/// Emits one finding when a line contains a configured token.
/// </summary>
public sealed class ContainsTokenRule : ICodeRule
{
    private readonly string _token;
    private readonly string _severity;
    private readonly string _category;
    private readonly string _description;
    private readonly string _fix;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContainsTokenRule"/> class.
    /// </summary>
    public ContainsTokenRule(string token, string severity, string category, string description, string fix)
    {
        _token = token;
        _severity = severity;
        _category = category;
        _description = description;
        _fix = fix;
    }

    /// <inheritdoc/>
    public ReviewIssue? Evaluate(RuleContext context)
    {
        var line = FindFirstLine(context.Lines, _token);
        if (line is null)
        {
            return null;
        }

        return new ReviewIssue(_severity, _category, line, _description, _fix);
    }

    private static int? FindFirstLine(IReadOnlyList<string> lines, string token)
    {
        for (var i = 0; i < lines.Count; i++)
        {
            if (lines[i].Contains(token, StringComparison.Ordinal))
            {
                return i + 1;
            }
        }

        return null;
    }
}
