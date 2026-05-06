using McpCodeReviewServer.Rules.Abstractions;
using McpCodeReviewServer.Rules.Async;
using McpCodeReviewServer.Rules.CSharpModernization;
using McpCodeReviewServer.Rules.FileAndFolder;
using McpCodeReviewServer.Rules.Maintainability;
using McpCodeReviewServer.Rules.Method;
using McpCodeReviewServer.Rules.Performance;
using McpCodeReviewServer.Rules.Security;
using McpCodeReviewServer.Rules.TypeDesign;
using Microsoft.Extensions.DependencyInjection;

namespace McpCodeReviewServer.Services;

/// <summary>
/// Registers review engine services and rule providers.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds code review services and all rule categories.
    /// </summary>
    /// <param name="services">Service collection instance.</param>
    /// <returns>The same service collection for chaining.</returns>
    public static IServiceCollection AddCodeReviewEngine(this IServiceCollection services)
    {
        services.AddSingleton<IReviewAnalyzer, ReviewAnalyzer>();
        services.AddSingleton<IReviewScorer, ReviewScorer>();
        services.AddSingleton<IRuleBacklogService, MarkdownRuleBacklogService>();

        services.AddSingleton<IRuleGroupProvider, AsyncRulesProvider>();
        services.AddSingleton<IRuleGroupProvider, SecurityRulesProvider>();
        services.AddSingleton<IRuleGroupProvider, PerformanceRulesProvider>();
        services.AddSingleton<IRuleGroupProvider, MaintainabilityRulesProvider>();
        services.AddSingleton<IRuleGroupProvider, MethodRulesProvider>();
        services.AddSingleton<IRuleGroupProvider, TypeDesignRulesProvider>();
        services.AddSingleton<IRuleGroupProvider, FileAndFolderRulesProvider>();
        services.AddSingleton<IRuleGroupProvider, CSharpModernizationRulesProvider>();

        return services;
    }
}
