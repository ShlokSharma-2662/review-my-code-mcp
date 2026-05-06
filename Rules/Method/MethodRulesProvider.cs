using McpCodeReviewServer.Models;
using McpCodeReviewServer.Rules.Abstractions;
using System.Text.RegularExpressions;

namespace McpCodeReviewServer.Rules.Method;

/// <summary>
/// Provides method-level design and quality rules.
/// </summary>
public sealed class MethodRulesProvider : IRuleGroupProvider
{
    private static readonly Regex MethodSignatureRegex = new(
        "\\b(public|private|protected|internal)\\s+(static\\s+)?(async\\s+)?[\\w<>,\\[\\]\\?]+\\s+(\\w+)\\s*\\(([^)]*)\\)",
        RegexOptions.Compiled);

    /// <inheritdoc/>
    public string Category => "method";

    /// <inheritdoc/>
    public IReadOnlyCollection<ICodeRule> BuildRules() =>
        new ICodeRule[]
        {
            new DelegateRule(EvaluateMethodNamingConventionRule),
            new DelegateRule(EvaluateParameterCountRule)
        };

    private static ReviewIssue? EvaluateMethodNamingConventionRule(RuleContext context)
    {
        for (var i = 0; i < context.Lines.Count; i++)
        {
            var line = context.Lines[i];
            var match = MethodSignatureRegex.Match(line);
            if (!match.Success)
            {
                continue;
            }

            var methodName = match.Groups[4].Value;
            if (IsPascalCase(methodName))
            {
                continue;
            }

            return new ReviewIssue(
                "suggestion",
                "method",
                i + 1,
                "Method name does not follow .NET PascalCase naming conventions.",
                "Rename the method to PascalCase (for example, CalculateScore) and avoid underscores.");
        }

        return null;
    }

    private static ReviewIssue? EvaluateParameterCountRule(RuleContext context)
    {
        for (var i = 0; i < context.Lines.Count; i++)
        {
            var line = context.Lines[i];
            var match = MethodSignatureRegex.Match(line);
            if (!match.Success)
            {
                continue;
            }

            var parameterSection = match.Groups[5].Value.Trim();
            if (string.IsNullOrEmpty(parameterSection))
            {
                continue;
            }

            var parameterCount = parameterSection.Split(',', StringSplitOptions.RemoveEmptyEntries).Length;
            if (parameterCount <= 5)
            {
                continue;
            }

            return new ReviewIssue(
                "suggestion",
                "method",
                i + 1,
                "Method has a high number of parameters, which can reduce readability and testability.",
                "Consider introducing a request object or grouping related parameters into a dedicated model.");
        }

        return null;
    }

    private static bool IsPascalCase(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        if (name.Contains('_', StringComparison.Ordinal))
        {
            return false;
        }

        return char.IsUpper(name[0]);
    }
}
