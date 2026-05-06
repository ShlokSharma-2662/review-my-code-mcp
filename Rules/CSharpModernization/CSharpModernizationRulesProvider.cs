using McpCodeReviewServer.Models;
using McpCodeReviewServer.Rules.Abstractions;

namespace McpCodeReviewServer.Rules.CSharpModernization;

/// <summary>
/// Provides modernization suggestions for C# language features.
/// </summary>
public sealed class CSharpModernizationRulesProvider : IRuleGroupProvider
{
    /// <inheritdoc/>
    public string Category => "c# 10 usage";

    /// <inheritdoc/>
    public IReadOnlyCollection<ICodeRule> BuildRules() =>
        new ICodeRule[]
        {
            new DelegateRule(EvaluateTopLevelStatementRule)
        };

    private static ReviewIssue? EvaluateTopLevelStatementRule(RuleContext context)
    {
        var mainLine = FindFirstLine(context.Lines, "public static void Main(");
        if (mainLine is null)
        {
            return null;
        }

        var hasAwait = context.Lines.Any(line => line.Contains("await ", StringComparison.Ordinal));
        if (hasAwait)
        {
            return null;
        }

        return new ReviewIssue(
            "suggestion",
            "c# 10 usage",
            mainLine,
            "Top-level statements can simplify entry points in modern C#.",
            "Replace explicit Main boilerplate with top-level statements where appropriate.");
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
