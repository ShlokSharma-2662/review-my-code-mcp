using McpCodeReviewServer.Models;
using McpCodeReviewServer.Services;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace McpCodeReviewServer.Tools;

/// <summary>
/// Exposes MCP tool endpoints for source code review and rule backlog discovery.
/// </summary>
[McpServerToolType]
public sealed class CodeReviewTool
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly IReviewAnalyzer _reviewAnalyzer;
    private readonly IReviewScorer _reviewScorer;
    private readonly IRuleBacklogService _ruleBacklogService;
    private readonly ILogger<CodeReviewTool> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CodeReviewTool"/> class.
    /// </summary>
    public CodeReviewTool(
        IReviewAnalyzer reviewAnalyzer,
        IReviewScorer reviewScorer,
        IRuleBacklogService ruleBacklogService,
        ILogger<CodeReviewTool> logger)
    {
        _reviewAnalyzer = reviewAnalyzer;
        _reviewScorer = reviewScorer;
        _ruleBacklogService = ruleBacklogService;
        _logger = logger;
    }

    /// <summary>
    /// Reviews C# source code and returns a JSON report with severity, category, line, description, and fix suggestions.
    /// </summary>
    /// <param name="code">Raw C# source code to review.</param>
    /// <param name="maxIssues">Maximum number of issues to return in the report.</param>
    /// <returns>A JSON string with summary, score, and issues.</returns>
    [McpServerTool(Name = "review_csharp_code")]
    [Description("Reviews C# code for correctness, security, performance, maintainability, async behavior, and design rules. Returns structured JSON.")]
    public string ReviewCSharpCode(
        [Description("Raw C# source code to review.")] string code,
        [Description("Maximum number of issues to return.")] int maxIssues = 50)
    {
        try
        {
            var invocationId = Guid.NewGuid().ToString("N");

            if (string.IsNullOrWhiteSpace(code))
            {
                var emptyResult = new ReviewResult(
                    "No C# code was provided for review.",
                    0,
                    new[]
                    {
                        new ReviewIssue(
                            "warning",
                            "correctness",
                            null,
                            "Input code is empty.",
                            "Provide non-empty C# code to review.")
                    },
                    invocationId,
                    0,
                    0,
                    Array.Empty<string>(),
                    Array.Empty<CategoryReviewScore>(),
                    Array.Empty<SuggestedChange>());

                return JsonSerializer.Serialize(emptyResult, JsonOptions);
            }

            var analysis = _reviewAnalyzer.Analyze(code, maxIssues);
            var score = _reviewScorer.CalculateScore(analysis.AllIssues);
            var categoryScores = _reviewScorer.CalculateCategoryScores(analysis.CategoryAnalyses, analysis.AllIssues);
            var checkedCategories = analysis.CategoryAnalyses
                .Select(category => category.Category)
                .OrderBy(category => category, StringComparer.OrdinalIgnoreCase)
                .ToArray();

            var totalRulesChecked = analysis.CategoryAnalyses.Sum(category => category.RulesChecked);
            var totalRulesMatched = analysis.CategoryAnalyses.Sum(category => category.RulesMatched);

            var suggestedChanges = analysis.Issues
                .Select(issue => new SuggestedChange(
                    issue.Severity,
                    issue.Category,
                    issue.Line,
                    issue.Description,
                    issue.Fix))
                .ToArray();

            var summary = analysis.AllIssues.Count == 0
                ? "Code looks clean with no major issues."
                : analysis.AllIssues.Count > analysis.Issues.Count
                    ? $"Detected {analysis.AllIssues.Count} issue(s) across review dimensions. Returning top {analysis.Issues.Count} based on maxIssues={Math.Max(1, maxIssues)}."
                    : $"Detected {analysis.AllIssues.Count} issue(s) across review dimensions.";

            _logger.LogInformation(
                "review_csharp_code invoked. InvocationId={InvocationId}, InputChars={InputChars}, MaxIssues={MaxIssues}, RulesChecked={RulesChecked}, RulesMatched={RulesMatched}, TotalIssues={TotalIssues}",
                invocationId,
                code.Length,
                maxIssues,
                totalRulesChecked,
                totalRulesMatched,
                analysis.AllIssues.Count);

            return JsonSerializer.Serialize(new ReviewResult(
                summary,
                score,
                analysis.Issues,
                invocationId,
                totalRulesChecked,
                totalRulesMatched,
                checkedCategories,
                categoryScores,
                suggestedChanges), JsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while reviewing C# code.");

            var invocationId = Guid.NewGuid().ToString("N");

            var errorResult = new ReviewResult(
                "Review failed due to an internal error.",
                0,
                new[]
                {
                    new ReviewIssue(
                        "critical",
                        "correctness",
                        null,
                        $"Internal review error: {ex.Message}",
                        "Retry with valid C# input. If the issue persists, inspect server logs.")
                },
                invocationId,
                0,
                0,
                Array.Empty<string>(),
                Array.Empty<CategoryReviewScore>(),
                Array.Empty<SuggestedChange>());

            return JsonSerializer.Serialize(errorResult, JsonOptions);
        }
    }

    /// <summary>
    /// Returns a lightweight health status payload for operational checks.
    /// </summary>
    /// <param name="message">Optional message to include in the health response.</param>
    /// <returns>A JSON string indicating server health.</returns>
    [McpServerTool(Name = "health_check")]
    [Description("Returns server health status and UTC timestamp.")]
    public string HealthCheck([Description("Optional message to include in the response.")] string? message = null)
    {
        try
        {
            var payload = new
            {
                status = "ok",
                timestampUtc = DateTime.UtcNow,
                message = string.IsNullOrWhiteSpace(message) ? "MCP server is healthy." : message
            };

            return JsonSerializer.Serialize(payload, JsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Health check failed.");
            return "{\"status\":\"error\",\"message\":\"Health check failed.\"}";
        }
    }

    /// <summary>
    /// Reads proposed rules from markdown files under documentation/rule-catalog.
    /// </summary>
    /// <returns>A JSON object with pending rules grouped by markdown file.</returns>
    [McpServerTool(Name = "get_rule_backlog")]
    [Description("Reads Add New Rules sections from markdown files in documentation/rule-catalog and returns pending rule proposals as JSON.")]
    public string GetRuleBacklog()
    {
        try
        {
            var pending = _ruleBacklogService.ReadPendingRules();
            return JsonSerializer.Serialize(pending, JsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed reading rule backlog.");
            return "{\"status\":\"error\",\"message\":\"Failed to read rule backlog.\"}";
        }
    }
}
