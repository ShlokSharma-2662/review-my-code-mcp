using McpCodeReviewServer.Rules.Abstractions;
using System.Text.RegularExpressions;

namespace McpCodeReviewServer.Rules.Security;

/// <summary>
/// Provides security-focused rules.
/// </summary>
public sealed class SecurityRulesProvider : IRuleGroupProvider
{
    private static readonly Regex HardcodedCredentialRegex = new(
        "(password|pwd|secret|apikey|api_key)\\s*=\\s*\"[^\"]+\"",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <inheritdoc/>
    public string Category => "security";

    /// <inheritdoc/>
    public IReadOnlyCollection<ICodeRule> BuildRules() =>
        new ICodeRule[]
        {
            new ContainsTokenRule(
                "SELECT \" +",
                "warning",
                "security",
                "Potential SQL injection risk from string concatenation in SQL commands.",
                "Use parameterized queries with DbParameter, Dapper parameters, or EF Core LINQ."),
            new ContainsTokenRule(
                "FromSqlRaw(",
                "warning",
                "security",
                "Raw SQL execution can be unsafe when user input is interpolated.",
                "Use FromSqlInterpolated or parameterized FromSqlRaw with explicit parameters."),
            new ContainsTokenRule(
                "Process.Start(",
                "warning",
                "security",
                "Launching processes with unsanitized input can lead to command injection.",
                "Validate and whitelist inputs. Prefer fixed executable paths and argument escaping."),
            new RegexRule(
                HardcodedCredentialRegex,
                "critical",
                "security",
                "Possible hardcoded credential or secret found.",
                "Move secrets to secure configuration providers (environment variables, user secrets, Key Vault).")
        };
}
