using McpCodeReviewServer.Models;
using McpCodeReviewServer.Rules.Abstractions;
using System.Text.RegularExpressions;

namespace McpCodeReviewServer.Rules.FileAndFolder;

/// <summary>
/// Provides file and folder organization rules.
/// </summary>
public sealed class FileAndFolderRulesProvider : IRuleGroupProvider
{
    private static readonly Regex NamespaceDeclarationRegex = new(
        "^\\s*namespace\\s+([A-Za-z0-9_.]+)\\s*;?\\s*$",
        RegexOptions.Compiled);

    /// <inheritdoc/>
    public string Category => "file-folder";

    /// <inheritdoc/>
    public IReadOnlyCollection<ICodeRule> BuildRules() =>
        new ICodeRule[]
        {
            new ContainsTokenRule(
                "namespace ",
                "suggestion",
                "file-folder",
                "Consider file-scoped namespaces for cleaner C# 10 syntax.",
                "Use: namespace YourNamespace;"),
            new DelegateRule(EvaluateNamespaceNamingConventionRule),
            new DelegateRule(EvaluateLargeFileRule),
            new DelegateRule(EvaluateManyTypeDeclarationsRule)
        };

    private static ReviewIssue? EvaluateNamespaceNamingConventionRule(RuleContext context)
    {
        for (var i = 0; i < context.Lines.Count; i++)
        {
            var line = context.Lines[i];
            var match = NamespaceDeclarationRegex.Match(line);
            if (!match.Success)
            {
                continue;
            }

            var namespaceValue = match.Groups[1].Value;
            var segments = namespaceValue.Split('.', StringSplitOptions.RemoveEmptyEntries);
            if (segments.All(IsNamespaceSegmentConventional))
            {
                continue;
            }

            return new ReviewIssue(
                "suggestion",
                "file-folder",
                i + 1,
                "Namespace naming does not follow conventional PascalCase segment style.",
                "Rename namespace segments to PascalCase and avoid underscores (for example, Company.Product.Feature).");
        }

        return null;
    }

    private static ReviewIssue? EvaluateLargeFileRule(RuleContext context)
    {
        if (context.Lines.Count <= 400)
        {
            return null;
        }

        return new ReviewIssue(
            "suggestion",
            "file-folder",
            1,
            "Large file detected; navigation and maintainability can degrade as file size grows.",
            "Split this file into focused types and move related concerns into dedicated folders.");
    }

    private static ReviewIssue? EvaluateManyTypeDeclarationsRule(RuleContext context)
    {
        var count = 0;
        for (var i = 0; i < context.Lines.Count; i++)
        {
            var line = context.Lines[i];
            if (line.Contains(" class ", StringComparison.Ordinal) ||
                line.Contains(" interface ", StringComparison.Ordinal) ||
                line.Contains(" record ", StringComparison.Ordinal))
            {
                count++;
            }

            if (count < 4)
            {
                continue;
            }

            return new ReviewIssue(
                "suggestion",
                "file-folder",
                i + 1,
                "Multiple type declarations found in one file.",
                "Prefer one primary type per file and organize by feature-focused folders.");
        }

        return null;
    }

    private static bool IsNamespaceSegmentConventional(string segment)
    {
        if (string.IsNullOrWhiteSpace(segment))
        {
            return false;
        }

        if (segment.Contains('_', StringComparison.Ordinal))
        {
            return false;
        }

        return char.IsUpper(segment[0]);
    }
}
