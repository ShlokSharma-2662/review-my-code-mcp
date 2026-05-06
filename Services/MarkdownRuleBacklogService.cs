using Microsoft.Extensions.Hosting;

namespace McpCodeReviewServer.Services;

/// <summary>
/// Parses markdown rule documents and extracts pending entries.
/// </summary>
public sealed class MarkdownRuleBacklogService : IRuleBacklogService
{
    private readonly string _rulesDirectory;

    /// <summary>
    /// Initializes a new instance of the <see cref="MarkdownRuleBacklogService"/> class.
    /// </summary>
    /// <param name="hostEnvironment">Host environment for resolving content root.</param>
    public MarkdownRuleBacklogService(IHostEnvironment hostEnvironment)
    {
        _rulesDirectory = Path.Combine(hostEnvironment.ContentRootPath, "documentation", "rule-catalog");
    }

    /// <inheritdoc/>
    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> ReadPendingRules()
    {
        var result = new Dictionary<string, IReadOnlyCollection<string>>(StringComparer.OrdinalIgnoreCase);
        if (!Directory.Exists(_rulesDirectory))
        {
            return result;
        }

        var files = Directory.GetFiles(_rulesDirectory, "*.md", SearchOption.TopDirectoryOnly);
        foreach (var file in files)
        {
            var entries = ExtractAddNewRuleEntries(file);
            result[Path.GetFileName(file)] = entries;
        }

        return result;
    }

    private static IReadOnlyCollection<string> ExtractAddNewRuleEntries(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        var inAddSection = false;
        var entries = new List<string>();

        foreach (var raw in lines)
        {
            var line = raw.Trim();

            if (line.StartsWith("## ", StringComparison.Ordinal) &&
                line.Equals("## Add New Rules", StringComparison.OrdinalIgnoreCase))
            {
                inAddSection = true;
                continue;
            }

            if (line.StartsWith("## ", StringComparison.Ordinal) &&
                !line.Equals("## Add New Rules", StringComparison.OrdinalIgnoreCase))
            {
                inAddSection = false;
                continue;
            }

            if (!inAddSection)
            {
                continue;
            }

            if (!line.StartsWith("- ", StringComparison.Ordinal))
            {
                continue;
            }

            entries.Add(line[2..].Trim());
        }

        return entries;
    }
}
