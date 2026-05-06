using McpCodeReviewServer.Models;
using System.Text.RegularExpressions;

namespace McpCodeReviewServer.Rules.Abstractions;

/// <summary>
/// Emits one finding when a regex matches any source line.
/// </summary>
public sealed class RegexRule : ICodeRule
{
    private readonly Regex _regex;
    private readonly string _severity;
    private readonly string _category;
    private readonly string _description;
    private readonly string _fix;

    /// <summary>
    /// Initializes a new instance of the <see cref="RegexRule"/> class.
    /// </summary>
    public RegexRule(Regex regex, string severity, string category, string description, string fix)
    {
        _regex = regex;
        _severity = severity;
        _category = category;
        _description = description;
        _fix = fix;
    }

    /// <inheritdoc/>
    public ReviewIssue? Evaluate(RuleContext context)
    {
        for (var i = 0; i < context.Lines.Count; i++)
        {
            if (_regex.IsMatch(context.Lines[i]))
            {
                return new ReviewIssue(_severity, _category, i + 1, _description, _fix);
            }
        }

        return null;
    }
}
