namespace McpCodeReviewServer.Services;

/// <summary>
/// Reads pending rule ideas from markdown rule documents.
/// </summary>
public interface IRuleBacklogService
{
    /// <summary>
    /// Reads add-new-rule entries by category file.
    /// </summary>
    /// <returns>Category file to pending rule entries map.</returns>
    IReadOnlyDictionary<string, IReadOnlyCollection<string>> ReadPendingRules();
}
