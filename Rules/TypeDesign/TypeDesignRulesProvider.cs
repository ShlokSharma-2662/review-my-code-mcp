using McpCodeReviewServer.Models;
using McpCodeReviewServer.Rules.Abstractions;
using System.Text.RegularExpressions;

namespace McpCodeReviewServer.Rules.TypeDesign;

/// <summary>
/// Provides class, interface, and record design rules.
/// </summary>
public sealed class TypeDesignRulesProvider : IRuleGroupProvider
{
    private static readonly Regex InterfaceNamingRegex = new(
        "\\binterface\\s+([A-Z][A-Za-z0-9_]*)",
        RegexOptions.Compiled);

    /// <inheritdoc/>
    public string Category => "type design";

    /// <inheritdoc/>
    public IReadOnlyCollection<ICodeRule> BuildRules() =>
        new ICodeRule[]
        {
            new ContainsTokenRule(
                "class ",
                "suggestion",
                "type design",
                "Consider record or record struct for immutable data carriers.",
                "For DTO-like types, use record or record struct with init-only properties."),
            new DelegateRule(EvaluateInterfaceNamingRule)
        };

    private static ReviewIssue? EvaluateInterfaceNamingRule(RuleContext context)
    {
        for (var i = 0; i < context.Lines.Count; i++)
        {
            var match = InterfaceNamingRegex.Match(context.Lines[i]);
            if (!match.Success)
            {
                continue;
            }

            var interfaceName = match.Groups[1].Value;
            if (interfaceName.StartsWith("I", StringComparison.Ordinal))
            {
                continue;
            }

            return new ReviewIssue(
                "suggestion",
                "type design",
                i + 1,
                "Interface naming does not follow common .NET conventions.",
                "Prefix interface names with 'I' (for example, IReviewAnalyzer).");
        }

        return null;
    }
}
