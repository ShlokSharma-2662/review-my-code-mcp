using McpCodeReviewServer.Models;
using McpCodeReviewServer.Rules.Abstractions;

namespace McpCodeReviewServer.Services;

/// <summary>
/// Executes registered rule sets and returns bounded findings.
/// </summary>
public sealed class ReviewAnalyzer : IReviewAnalyzer
{
    private readonly IReadOnlyCollection<RegisteredRule> _rules;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReviewAnalyzer"/> class.
    /// </summary>
    /// <param name="ruleGroups">Rule group providers discovered from DI.</param>
    public ReviewAnalyzer(IEnumerable<IRuleGroupProvider> ruleGroups)
    {
        _rules = ruleGroups
            .SelectMany(group => group.BuildRules().Select(rule => new RegisteredRule(group.Category, rule)))
            .ToArray();
    }

    /// <inheritdoc/>
    public ReviewAnalysisResult Analyze(string code, int maxIssues)
    {
        var normalizedMax = Math.Max(1, maxIssues);
        var lines = NormalizeLines(code);
        var context = new RuleContext(code, lines);

        var allIssues = new List<ReviewIssue>();
        var categoryCoverage = new Dictionary<string, CategoryCounter>(StringComparer.OrdinalIgnoreCase);

        foreach (var rule in _rules)
        {
            var category = string.IsNullOrWhiteSpace(rule.Category) ? "uncategorized" : rule.Category;
            if (!categoryCoverage.TryGetValue(category, out var counter))
            {
                counter = new CategoryCounter();
                categoryCoverage[category] = counter;
            }

            counter.RulesChecked++;

            var issue = rule.Evaluate(context);
            if (issue is null)
            {
                continue;
            }

            counter.RulesMatched++;
            allIssues.Add(issue);
        }

        var returnedIssues = allIssues.Take(normalizedMax).ToArray();
        var categoryAnalyses = categoryCoverage
            .Select(entry => new CategoryAnalysis(entry.Key, entry.Value.RulesChecked, entry.Value.RulesMatched))
            .OrderBy(entry => entry.Category, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        return new ReviewAnalysisResult(allIssues, returnedIssues, categoryAnalyses);
    }

    private static IReadOnlyList<string> NormalizeLines(string code) =>
        code.Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace('\r', '\n')
            .Split('\n');

    private sealed class RegisteredRule
    {
        public RegisteredRule(string category, ICodeRule rule)
        {
            Category = category;
            Rule = rule;
        }

        public string Category { get; }

        public ICodeRule Rule { get; }

        public ReviewIssue? Evaluate(RuleContext context) => Rule.Evaluate(context);
    }

    private sealed class CategoryCounter
    {
        public int RulesChecked { get; set; }

        public int RulesMatched { get; set; }
    }
}
